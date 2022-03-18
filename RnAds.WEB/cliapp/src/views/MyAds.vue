<template>
  <div v-if="email === ''">войдите или зарегистрируйтесь</div>
  <div v-else>
    <v-container>
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
              <v-chip class="ma-2" large> {{ ad.price }} &#8381;</v-chip>
              <v-card-actions>
                <v-btn
                  color="primary"
                  text
                  :to="`/search/${ad.typeAd}/${ad.id}`"
                >
                  Открыть
                </v-btn>
                <v-btn
                  class="ma-2"
                  outlined
                  rounded
                  color="success"
                  :to="`/editAd/${ad.id}`"
                >
                  Редактировать
                </v-btn>

                <v-spacer></v-spacer>

                <v-btn icon @click="editShow(ad)">
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
              :total-visible="7"
              @input="fetchAllAdsCurrentUser"
            ></v-pagination>
          </v-col>
        </v-row>
      </v-container>
    </v-footer>
  </div>
</template>

<script>
import SiteUrl from "../settings/site-url.settings";

export default {
  data: () => ({
    //ads:[],
    results: [],
    page: 1,
    paginationLength: 1,
  }),
  computed: {
    email: function () {
      return this.$store.getters.getEmail;
    },
  },
  created() {
    this.fetchAllAdsCurrentUser();
  },
  methods: {
    async fetchAllAdsCurrentUser() {
      const token = sessionStorage.getItem("accessToken");
      const response = await fetch(SiteUrl.getAdCurrentUser()+this.page, {
        method: "GET",
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + token,
        },
      });
      if (response.ok === true) {
        let data = await response.json();
        this.results = data.map((item) => {
          item.show = false;
          return item;
        });
      } else console.log("Status: ", response.status);
    },
    getUrlPicture(id) {
      return `${SiteUrl.getPicture()}${id}`;
    },
    editShow(ad) {
      ad.show = !ad.show;
      console.log(ad);
    },
  },
};
</script>
