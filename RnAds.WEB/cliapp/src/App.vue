<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" app>
      <v-container>
        <v-row justify="center">
          <v-col cols="auto">
            <v-avatar class="mb-4" color="grey darken-1" size="64">
              <img :src="avatar" width="40" height="40"
            /></v-avatar>
            <div v-if="email === ''">
              <v-btn text color="primary" :to="`/jwt`">вход</v-btn>
              <v-btn text color="primary" :to="`/register`">регистрация</v-btn>
            </div>
            <div v-else>
              {{ email }}
              <v-btn icon color="red" @click="LogOut">
                <v-icon>mdi-close-circle</v-icon>
              </v-btn>
            </div>
          </v-col>
        </v-row>
      </v-container>

      <v-list dense>
        <v-list-item-group v-model="selectedMenuItem">
          <template
            v-for="item in routes.filter((e) => e.panel === 'Основные модули')"
          >
            <v-list-item :key="item.name" link :to="item.path">
              <v-list-item-action>
                <v-icon>{{ item.icon }}</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  {{ item.name }}
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </template>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app height="120">
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <img :src="require('./assets/rn.png')" />
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <v-btn text :to="`/`">
        <h1>РН - объявления</h1>
      </v-btn>
      <v-container>
        <v-row style="height: 15px"></v-row>
        <v-row>
          <v-col md="3">
            <v-select :items="types" v-model="category" dense outlined>
              <template slot="label">
                <v-icon>mdi-format-list-bulleted-type</v-icon> Выберите
                категорию
              </template>
            </v-select>
            <v-switch
              v-model="switch1"
              label="Только с фото"
              @change="ChangeSwitch"
            ></v-switch>
          </v-col>
          <v-col md="4">
            <v-text-field dense outlined v-model="text">
              <template slot="label">
                <v-icon>mdi-magnify</v-icon> Поиск по объявлениям
              </template>
            </v-text-field>
          </v-col>
          <v-col md="3">
            <v-autocomplete
              v-model="city"
              :items="regions"
              dense
              outlined
              label="Filled"
            >
              <template slot="label">
                <v-icon small>mdi-map-search</v-icon> Выберите город или регион
              </template></v-autocomplete
            >
          </v-col>
          <v-col md="2">
            <v-btn
              v-on:click="fetchGetSpecifyAd(category, city, text, isPhoto)"
            >
              Найти
            </v-btn>
          </v-col>
        </v-row>
      </v-container>
    </v-app-bar>
    <v-container v-show="cityVisible">
      <v-row style="height: 150px"></v-row>
      <v-row>
        <v-col md="4"></v-col>
        <v-col md="5"></v-col>
        <v-col md="3">
          <v-card class="mx-auto" max-width="300" outlined>
            <v-card-title> {{ cityCurrent }}<br />Это Ваш город? </v-card-title>
            <v-card-actions>
              <v-btn color="primary" @click="SetCity"> Да </v-btn>
              <v-spacer></v-spacer>
              <v-btn outlined text @click="cityVisible = false">
                Изменить
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>

    <v-main>
      <v-container fluid class="page-body h-100">
        <v-row no-gutters class="h-100">
          <v-col class="h-100" id="page-wrapper">
            <router-view></router-view>
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import ApiService from "./services/api.service";
import SiteUrl from "./settings/site-url.settings";

export default {
  name: "App",

  computed: {
    routes() {
      return this.$router.options.routes.filter((e) => e.display);
    },

    category: {
      get: function () {
        return this.SetType();
      },
      set: function (newValue) {
        this.$store.commit("setTypeAd", newValue);
      },
    },
    email: {
      get: function () {
        return this.$store.getters.getEmail;
      },
      set: function (setEmail) {
        this.$store.commit("setEmail", setEmail);
      },
    },
    avatar: {
      get: function () {
        return this.$store.getters.getAvatar;
      },
      set: function (setAvatar) {
        this.$store.commit("setAvatar", setAvatar);
      },
    },
  },
  data: () => ({
    drawer: null,
    selectedMenuItem: 0,
    types: [
      "Любая категория",
      "Животные",
      "Бизнес и оборудование для бизнеса",
      "Электроника",
      "Для дома и дачи",
      "Хобби и отдых",
      "Личные вещи",
      "Недвижимость",
      "Резюме",
      "Сервис",
      "Транспорт",
      "Работа",
      "Запасные части и аксессуары",
    ],
    switch1: false,
    city: "",
    cityCurrent: "",
    regions: [],
    cityVisible: false,
    cityWath: "",
    currentType: "",
    results: [],
    currentTypeAd: "",
    text: "",
    isPhoto: false,
    page: 1,
    paginationLength: 1,
    allAdsCount:0
  }),
  watch: {
    city: function (setCity) {
      this.$store.commit("setCity", setCity);
    },
    results: function (setAdsList) {
      this.$store.commit("setAdsList", setAdsList);
    },
    allAdsCount: function (setAdsCount) {
      this.$store.commit("setAdsCount", setAdsCount);
    },
    text: function (setText) {
      this.$store.commit("setText", setText);
    },
    isPhoto: function (setIsPhoto) {
      this.$store.commit("setIsPhoto", setIsPhoto);
    }
  },
  created() {
    this.fetchGetNamesRegion();
    this.fetchGetCityCurrentUser();
  },
  methods: {
    async fetchGetNamesRegion() {
      let r = await ApiService.get(SiteUrl.getNamesRegion(), false);

      if (r.error) alert(r.error);
      else {
        let items = r.data;
        this.regions = items;
      }
    },
    async fetchGetCityCurrentUser() {
      let r = await ApiService.get(SiteUrl.getCityCurrentUser(), false);

      if (r.error) alert(r.error);
      else {
        let item = r.data;
        this.cityCurrent = item;
        this.cityVisible = true;
      }
    },
    SetType() {
      if (this.$store.getters.getTypeAd != !null) {
        return (this.currentTypeAd = this.$store.getters.getTypeAd);
      } else return "тип";
    },
    SetCity() {
      this.city = this.cityCurrent;
      this.cityVisible = false;
    },

    async fetchGetSpecifyAd(typeAd, city, text, isPhoto) {
      let url = "1";
      if (typeAd === "") typeAd="Любая категория";
      if (typeAd) url += `&type=${typeAd}`;

      if (city) url += `&city=${city}`;

      if (text) url += `&text=${text}`;

      if (isPhoto) url += `&isPhoto=${isPhoto}`;

      let site = "";
      let r = null;

      if (this.email === "") {
        site = SiteUrl.getSpecifyAd();
        r = await ApiService.get(site + url, false);
        if (r.error) alert(r.error);
        else {
          let items = r.data;
          this.results = items.listAd.map((item) => {
            item.show = false;
            return item;
          });
          this.allAdsCount=items.countAds;
        }
      } else {
        site = SiteUrl.getSpecifyAdByAuthorizeUsers();
        const token = sessionStorage.getItem("accessToken");
        r = await fetch(site + url, {
          method: "GET",
          headers: {
            Accept: "application/json",
            Authorization: "Bearer " + token,
          },
        });
        if (r.ok === true) {
          const data = await r.json();
          this.results = data.listAd.map((item) => {
            item.show = false;
            return item;
          });
          this.allAdsCount=data.countAds;
        } else console.log("Status: ", r.status);
      }

      this.$router.push(`/search/${this.typeAd}`);
    },
    async fetchGetUser() {
      const token = sessionStorage.getItem("accessToken");
      const response = await fetch(SiteUrl.getCurrentUser(), {
        method: "GET",
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + token,
        },
      });
      if (response.ok === true) {
        const data = await response.json();
        alert(data);
      } else console.log("Status: ", response.status);
    },
    LogOut() {
      this.email = "";
      this.avatar = "";
    },
    ChangeSwitch() {
      this.isPhoto = !this.isPhoto;
    },
  },
};
</script>

<style scoped>
.page-body {
  padding: 1rem 1.5rem;
}

#page-wrapper {
  padding: 0.5rem;
}

.h-100 {
  height: 100%;
}

.bg-rn {
  background-color: #fecf00 !important;
}

.logo-wrapper {
  height: 40px;
  margin-right: 20px;
}
</style>
