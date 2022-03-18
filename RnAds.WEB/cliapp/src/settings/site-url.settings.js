const SiteUrl = {

    getAllAds: () => `https://localhost:44387/Ad/GetAllAd?page=`,
    getAdById: () => `https://localhost:44387/Ad/GetAdById`,
    getNamesRegion: () => `https://localhost:44387/Ad/GetAllRegionOrCityName`,
    getCityCurrentUser: () => `https://localhost:44387/User/GetCity`,
    getAdsCount: () => `https://localhost:44387/Ad/GetAdsCount?city=`,
    getSpecifyAd: () => `https://localhost:44387/Ad/GetAdByType?page=`,
    getAllAdRowCount: () => `https://localhost:44387/Ad/GetAllAdRowCount`,
    getPicture: () => 'https://localhost:44387/Picture/GetPicture/',

    createAd:() => 'https://localhost:44387/Ad/CreateAd',
    createPhoto:() => 'https://localhost:44387/Picture/AddedPhoto',
    
    updatePhoto:() => 'https://localhost:44387/Picture/UpdatedPhoto',

    userRegister:() => 'https://localhost:44387/Account/Register',
    addAvatarUser:()=> 'https://localhost:44387/Account/AddAvatarUsers',
    getCurrentUser:()=> 'https://localhost:44387/Account/GetCurrentUser',
    jwtTest:()=> 'https://localhost:44387/Account/Token',
    getAvatarUser:()=> 'https://localhost:44387/Account/GetAvatar',
    getAvatarOwner:()=> 'https://localhost:44387/Account/GetAvatarOwner?id=',
    editCurrentUser:()=> 'https://localhost:44387/Account/EditCurrentUser',

    EditAd:() => 'https://localhost:44387/Ad/UpdateAds',

    signalR:() => 'https://localhost:44387/Chat/Create',
    
    getAdCurrentUser:()=> 'https://localhost:44387/User/GetCurrentUserAd?page=',

    writeMessage:() => 'https://localhost:44387/Message/WriteMessage',
    getAllNamesChats:() => 'https://localhost:44387/Message/GetAllDialogs',

    getSpecifyAdByAuthorizeUsers: () => `https://localhost:44387/Ad/GetAdByTypeByAuthorizeUsers?page=`,
    getAllAdsByAuthorizeUsers: () => `https://localhost:44387/Ad/GetAllAdByAuthorizeUsers?page=`,

    addedFavorite:()=> 'https://localhost:44387/User/AddFavoriteAd?id=',
    deleteFavorite:()=> 'https://localhost:44387/User/DeleteFavoriteAd?id=',
    getAllFavorite:()=> 'https://localhost:44387/User/GetAllFavoriteAd?page=',

    createChatsMessage:() => 'https://localhost:44387/Message/SendChatMessage',

};

export default SiteUrl;