import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import MyAds from '../views/MyAds.vue'
import Add from '../views/AddAdvertisement.vue'
import Search from '../views/Search.vue'
import Favorite from '../views/FavoriteAds.vue'
import Ad from '../views/Ad.vue'
import Register from '../views/Register.vue'
import Jwt from '../views/JwtToken.vue'
import EditAd from '../views/EditMyAd.vue'
import Chats from '../views/Chats.vue'
import MyProfile from '../views/MyProfile.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
    display: true
  },
  {
    path: "/MyAds",
    name: "Мои объявления",
    component: MyAds,
    panel: "Основные модули",
    icon: "mdi-calendar-text-outline",
    display: true
  },
  {
    path: "/Favorite",
    name: "Избранные",
    component: Favorite,
    panel: "Основные модули",
    icon: "mdi-heart-multiple-outline",
    display: true
  },
  {
    path: "/Chats",
    name: "Сообщения",
    component: Chats,
    panel: "Основные модули",
    icon: "mdi-message-text-outline",
    display: true
  },
  {
    path: "/add",
    name: "Разместить объявление",
    component: Add ,
    panel: "Основные модули",
    icon: "mdi-plus-circle",
    display: true
  },
  {
    path: "/search/:category",
    name: "Search",
    component: Search,
    display: true,
    props:true
  },
  {
    path: "/MyProfile",
    name: "Мой профиль",
    component: MyProfile,
    panel: "Основные модули",
    icon: "mdi-card-account-details",
    display: true
  },
  {
    path: "/search/:category/:id",
    name: "Ad",
    component: Ad,
    display: true,
    props:true
  },
  {
    path: "/register",
    name: "Register",
    component: Register,
    display: true,
  },
  {
    path: "/jwt",
    name: "Jwt",
    component: Jwt,
    display: true,
  },
  {
    path: "/editAd/:id",
    name: "EditAd",
    component: EditAd,
    display: true,
    props:true
  }
  
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
