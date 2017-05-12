# Request resource group name
param (
    [Parameter(Mandatory = $true)]
    [string]$resourceGroupName,
    [timespan]$sasTokenDuration = "90:0:0:0"
)

# Find storage account in the resource group

$storageAccounts = Get-AzureRmStorageAccount -ResourceGroupName $resourceGroupName

# Ensure there is only one storage account
if($storageAccounts.Count -eq 0)
{
    Write-Error "The resource group '$($resourceGroup)' doesn't have a storage account in it"
}
elseif($storageAccounts.Count -gt 1)
{
    Write-Error "The resource group '$($resourceGroup)' has more than one storage account in it"
}
else
{
    $account = $storageAccounts[0]
    $accountContext = $account.Context
    if((Get-AzureStorageContainer -Context $accountContext |
        where {$_.Name -eq "user-files"}).Count -eq 0)
    { # There are no user-files container, let's create one
        New-AzureStorageContainer -Context $accountContext `
            -Name user-files -Permission Off
    }
    $userFilesContainerContext =
        (Get-AzureStorageContainer -Context $accountContext -Name "user-files").Context

    # Create SAS token
    $expiryDate = (Get-Date).Add($sasTokenDuration)
    $sasToken = New-AzureStorageContainerSASToken -Context $userFilesContainerContext `
        -Name "Read-Write-List" -ExpiryTime $expiryDate -Permission rwl

    # Format return object
    $props = @{
        UserFilesSasToken = $sasToken
        }
    $object = new-object psobject -Property $props

    return $object
}