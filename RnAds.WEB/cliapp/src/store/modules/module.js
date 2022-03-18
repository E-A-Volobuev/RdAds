const state = {
    city: '',
    typeAd:'',
    adsList:[],
    email:'',
    avatar:null,
    message:{},
    allAdsCount:0,
    text:"",
    isPhoto:false

};

const getters = {
    getCity(state) {
        return state.city;
    },
    getTypeAd(state){
        return state.typeAd;
    },
    getAdList(state){
        return state.adsList;
    },
    getEmail(state){
        return state.email;
    },
    getAvatar(state){
        return state.avatar;
    },
    getMessage(state){
        return state.message;
    },
    getAdsCount(state){
        return state.allAdsCount;
    },
    getText(state){
        return state.text;
    },
    getIsPhoto(state){
        return state.isPhoto;
    }
};

const actions = {};

const mutations = {
    setCity(state, item) {
       state.city=item;
    },
    setTypeAd(state,item){
        state.typeAd=item;
    },
    setAdsList(state,item){
        state.adsList=item;
    },
    setEmail(state,item){
        state.email=item;
    },
    setAvatar(state,item){
        state.avatar=item;
    },
    setMessage(state,item){
        state.message=item;
    },
    setAdsCount(state,item){
        state.allAdsCount=item;
    },
    setText(state,item){
        state.text=item;
    },
    setIsPhoto(state,item){
        state.isPhoto=item;
    }
};

export default {
    state,
    actions,
    mutations,
    getters,
};
