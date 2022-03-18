<template>
  <div>
    <v-container>
      <div>
        <v-row>
          <v-col v-for="ad in ads" :key="ad.id">
            <v-card class="mx-auto" max-width="500">
              <v-carousel height="300">
                <v-carousel-item
                  v-for="picId in ad.pictures"
                  :key="picId"
                  reverse-transition="fade-transition"
                  transition="fade-transition"
                >
                  <v-img
                    :src="getUrlPicture(picId)"
                    contain
                    max-height="300"
                  ></v-img>
                </v-carousel-item>
              </v-carousel>
              <v-list-item two-line>
                <v-list-item-content>
                  <v-list-item-title class="text-h5">
                    {{ ad.nameAdvertisement }}
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-divider class="mx-4"></v-divider>
              <v-list-item>
                <v-list-item-title>
                  <v-icon> mdi-home-city </v-icon>
                  {{ ad.city }}
                </v-list-item-title>
                <v-list-item-title>
                  <v-icon> mdi-map </v-icon>
                  {{ ad.country }}
                </v-list-item-title>
              </v-list-item>
              <v-divider class="mx-4"></v-divider>
              <v-card-actions>
                <v-chip class="ma-2" large> {{ ad.price }} &#8381;</v-chip>
                <v-spacer></v-spacer>
                <div v-if="email != ''">
                  <v-btn icon @click="AddedFavoriteAd(ad)">
                    <v-icon
                      v-if="ad.isFavorite === false"
                      color="grey lighten-1"
                    >
                      mdi-star-outline
                    </v-icon>
                    <v-icon v-else color="yellow darken-3"> mdi-star </v-icon>
                  </v-btn>
                </div>
              </v-card-actions>
              <v-card-actions>
                <v-btn
                  color="primary"
                  text
                  :to="`/search/${ad.typeAd}/${ad.id}`"
                >
                  Открыть
                </v-btn>

                <v-spacer></v-spacer>

                <v-btn icon @click="ad.show = !ad.show">
                  <v-icon>{{
                    ad.show ? "mdi-chevron-up" : "mdi-chevron-down"
                  }}</v-icon>
                </v-btn>
              </v-card-actions>
              <v-expand-transition>
                <div v-show="ad.show">
                  <v-divider></v-divider>

                  <v-card-text>
                     {{ ad.comment }}
                  </v-card-text>
                </div>
              </v-expand-transition>
            </v-card>
          </v-col>
        </v-row>
        <v-row justify="center">
          <v-col cols="auto">
            <v-alert
              v-model="addedFavorite"
              dismissible
              type="success"
              elevation="2"
              style="width: 800px"
            >
              объявление "{{ selectAd }}"" добавлено в избранные
            </v-alert>
          </v-col>
        </v-row>
        <v-row justify="center">
          <v-col cols="auto">
            <v-alert
              v-model="removeFavorite"
              dismissible
              type="error"
              elevation="2"
              style="width: 800px"
            >
              объявление "{{ selectAd }}"" удалено из избранных
            </v-alert>
          </v-col>
        </v-row>
      </div>
    </v-container>
    <v-footer app color="transparent" height="72">
      <v-container>
        <v-row justify="center">
          <v-col cols="auto">
            <v-pagination
              v-model="page"
              :length="paginationLength"
              circle
              @input="fetchGetSpecifyAd(category, city, text, isPhoto)"
            ></v-pagination>
          </v-col>
        </v-row>
      </v-container>
    </v-footer>
  </div>
</template>

<script>
import ApiService from "../services/api.service";
import SiteUrl from "../settings/site-url.settings";

export default {
  props: {
    category: {
      type: String,
      default: null,
    },
  },
  computed: {
    ads: {
      get: function () {
        var items = this.$store.getters.getAdList;
        return items;
      },
      set: function (newValue) {
        this.$store.commit("setAdsList", newValue);
      },
    },
    email: function () {
      return this.$store.getters.getEmail;
    },
    allAdsCount: function () {
      var count = this.$store.getters.getAdsCount;
      return count;
    },
    paginationLength: function () {
      let lenght = Math.ceil(this.allAdsCount / 6);
      return lenght;
    },
    city: function () {
      return this.$store.getters.getCity;
    },
    text: function () {
      return this.$store.getters.getText;
    },
    isPhoto: function () {
      return this.$store.getters.getIsPhoto;
    },
  },
  data: () => ({
    show: false,
    dialog: false,
    typeAd: "",
    selectAd: "",
    addedFavorite: false,
    removeFavorite: false,
    page: 1,
  }),
  watch: {
    typeAd: function (setTypeAd) {
      this.$store.commit("setTypeAd", setTypeAd);
    },
  },
  methods: {
    getUrlPicture(id) {
      return `${SiteUrl.getPicture()}${id}`;
    },
    AddedFavoriteAd(ad) {
      this.selectAd = ad.nameAdvertisement;
      ad.isFavorite = !ad.isFavorite;
      if (ad.isFavorite === true) {
        this.fetchAddedFavorite(ad);
      } else {
        this.fetchDeleteFavorite(ad);
      }
    },
    async fetchAddedFavorite(ad) {
      const token = sessionStorage.getItem("accessToken");
      let r = await fetch(SiteUrl.addedFavorite() + ad.id, {
        method: "GET",
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + token,
        },
      });
      if (r.ok === true) {
        this.addedFavorite = true;
        this.removeFavorite = false;
      } else console.log("Status: ", r.status);
    },
    async fetchDeleteFavorite(ad) {
      const token = sessionStorage.getItem("accessToken");
      let r = await fetch(SiteUrl.deleteFavorite() + ad.id, {
        method: "DELETE",
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + token,
        },
      });
      if (r.ok === true) {
        this.removeFavorite = true;
        this.addedFavorite = false;
      } else console.log("Status: ", r.status);
    },
    async fetchGetSpecifyAd(typeAd, city, text, isPhoto) {
      let url = this.page;
      if (typeAd === "null") typeAd="Любая категория";
      if (typeAd === "undefined") typeAd="Любая категория";
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
          this.ads = items.listAd.map((item) => {
            item.show = false;
            return item;
          });
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
          this.ads = data.listAd.map((item) => {
            item.show = false;
            return item;
          });
        } else console.log("Status: ", r.status);
      }
    },
  },
};
</script>
