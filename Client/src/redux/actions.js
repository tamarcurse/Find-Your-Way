
export function updateAllProvider(newProvider) {

    return { type: 'UPDATE_ALL_PROVIDER', payLoad: newProvider }
}
export function updatePrivateProvider(newProvider) {

    return { type: 'UPDATE_PROVIDER_PRIVATE', payLoad: newProvider }
}
export function updatePrivateProviderAndSaveDB(newProvider) {

    return { type: 'UPDATE_PROVIDER_PRIVATE_AND_SAVE_DB', payLoad: newProvider }
}
export function updateFactoryPrivate(newProvider) {

    return { type: 'UPDATE_FACTORYP_RIVATE', payLoad: newProvider }
}
export function updateShops(newShops) {
    return { type: 'UPDATE_ALL_SHOP', payLoad: newShops }
}
export function updateTrucks(newShops) {
    return { type: 'UPDATE_ALL_TRUCKS', payLoad: newShops }
}
export function updateSizeContians(newShops) {
    return { type: 'UPDATE_ALL_SIZE_CONTAIN', payLoad: newShops }
}
export function updateOneShop(newShop) {
    return { type: 'UPDATE_ONE_SHOP', payLoad: newShop }
}