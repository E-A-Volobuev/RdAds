<template>
  <div>
    <v-container>
      <v-row style="height: 30px">
        <v-col>
          Все объявления в регионе: {{ region }} &nbsp;&nbsp;&nbsp;
          {{ allAdsCount }}
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Животные', region)"
          >
            Животные </v-btn
          >{{ adCount[0] }}<br />
          <v-btn
            color="primary"
            text
            @click="
              SelectCategory(
                'Бизнес и оборудование для бизнеса',
                region,
                false
              )
            "
          >
            Бизнес и оборудование для бизнеса </v-btn
          >{{ adCount[1] }}<br />
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Электроника', region)"
          >
            Электроника </v-btn
          >{{ adCount[2] }}
        </v-col>
        <v-col>
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Для дома и дачи', region)"
          >
            Для дома и дачи </v-btn
          >{{ adCount[3] }}<br />
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Хобби и отдых', region)"
          >
            Хобби и отдых </v-btn
          >{{ adCount[4] }}<br />
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Личные вещи', region)"
          >
            Личные вещи </v-btn
          >{{ adCount[5] }}
        </v-col>
        <v-col>
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Недвижимость', region)"
          >
            Недвижимость </v-btn
          >{{ adCount[6] }}<br />
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Резюме', region)"
          >
            Резюме </v-btn
          >{{ adCount[7] }}<br />
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Сервис', region)"
          >
            Услуги </v-btn
          >{{ adCount[8] }}
        </v-col>
        <v-col>
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Транспорт', region)"
          >
            Транспорт </v-btn
          >{{ adCount[9] }}<br />
          <v-btn
            color="primary"
            text
            @click="SelectCategory('Работа', region)"
          >
            Работа
          </v-btn>
          {{ adCount[10] }} <br />
          <v-btn
            color="primary"
            text
            @click="
              SelectCategory('Запасные части и аксессуары', region)
            "
          >
            Запасные части и аксессуары </v-btn
          >{{ adCount[11] }}
        </v-col>
      </v-row>

      <div>
        <v-row>
          <v-col v-for="ad in results" :key="ad.id">
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
                    Здесь комментарий к объявлению: {{ ad.comment }}
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
              @input="fetchAllAds"
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
  name: "home",

  data: () => ({
    results: [],
    adCount: [],
    allAdsCount: 0,
    allAdsCountResponce: 0,
    typeAd: "",
    selectAd: "",
    addedFavorite: false,
    removeFavorite: false,
    page: 1,
    paginationLength: 1,
  }),
  computed: {
    region: function () {
      this.fetchGetAdsCount(this.$store.getters.getCity);
      this.fetchGetSpecifyAd(null, this.$store.getters.getCity, true);
      return this.$store.getters.getCity;
    },
    category: function () {
      return this.$store.getters.getTypeAd;
    },
    email: function () {
      return this.$store.getters.getEmail;
    },
  },
  watch: {
    region(newValue, oldValue) {
      if (newValue !== oldValue) {
        this.fetchGetAdsCount(newValue);
        this.fetchGetSpecifyAd(null, newValue, true);
      }
    },
    typeAd: function (setTypeAd) {
      this.$store.commit("setTypeAd", setTypeAd);
    },
    results: function (setAdsList) {
      this.$store.commit("setAdsList", setAdsList);
    },
    allAdsCount: function (setAdsCount) {
      this.$store.commit("setAdsCount", setAdsCount);
    },
  },
  created() {
    this.fetchAllAds();
  },
  methods: {
    async fetchAllAds() {
      let city = this.$store.getters.getCity;

      if (city === "") {
        this.fetchAllAdsHelper();
      } else {
        this.fetchGetAdsCount(this.$store.getters.getCity);
        this.fetchGetSpecifyAd(null, this.$store.getters.getCity, true);
      }
    },
    async fetchAllAdsHelper() {
      let site = "";
      let r = null;

      if (this.email === "") {
        site = SiteUrl.getAllAds();
        r = await ApiService.get(site + this.page, false);

        if (r.error) alert(r.error);
        else {
          this.results = r.data.listAd.map((item) => {
            item.show = false;
            return item;
          });
          this.allAdsCount = r.data.countAds;
        }
      } else {
        site = SiteUrl.getAllAdsByAuthorizeUsers();
        const token = sessionStorage.getItem("accessToken");
        r = await fetch(site + this.page, {
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
          this.allAdsCount = data.countAds;
        } else console.log("Status: ", r.status);
      }
      this.paginationLength = Math.ceil(this.allAdsCount / 6);
    },
    async fetchGetAdsCount(city) {
      let r = await ApiService.get(SiteUrl.getAdsCount() + city, false);

      if (r.error) alert(r.error);
      else {
        let items = r.data;
        this.adCount = items;
      }
    },
    async fetchGetSpecifyAd(typeAd, city) {
      let url = "";
      if (typeAd) url += `&type=${typeAd}`;

      if (city) url += `&city=${city}`;

      let site = "";
      let r = null;

      if (this.email === "") {
        site = SiteUrl.getSpecifyAd();
        r = await ApiService.get(site + this.page + url, false);
        if (r.error) alert(r.error);
        else {
          let items = r.data;
          this.results = items.listAd.map((item) => {
            item.show = false;
            return item;
          });
          this.allAdsCount = items.countAds;
        }
      } else {
        site = SiteUrl.getSpecifyAdByAuthorizeUsers();
        const token = sessionStorage.getItem("accessToken");
        r = await fetch(site + this.page + url, {
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
          this.allAdsCount = data.countAds;
        } else console.log("Status: ", r.status);
      }

      this.paginationLength = Math.ceil(this.allAdsCount / 6);
    },
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
    async SelectCategory(typeAd,city) {
      await this.fetchGetSpecifyAd(typeAd, city);
      this.typeAd = typeAd;
      this.$router.push(`/search/${this.typeAd}`);
    },
  },
};
</script>