<template>
  <div>
    <v-container>
      <div v-for="ad in listAd" :key="ad.id">
        <v-row>
          <v-col>
            <v-card class="mx-auto" max-width="800">
              <v-img
                src="https://cdn.vuetifyjs.com/images/cards/sunshine.jpg"
                height="500px"
              ></v-img>

              <v-card-title> {{ ad.nameAdvertisement }} </v-card-title>

              <v-card-subtitle> {{ ad.city }}</v-card-subtitle>

              <v-card-subtitle>{{ ad.country }}</v-card-subtitle>

              <v-card-title>{{ ad.price }}</v-card-title>

              <v-card-actions>
                <v-btn
                  color="orange lighten-2"
                  text
                  v-on:click="fetchGetAdvertisement(ad)"
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
      </div>

      <v-row justify="center">
        <v-col cols="auto">
          <v-dialog
            v-model="dialog"
            transition="dialog-bottom-transition"
            max-width="800"
          >
            <v-card class="mx-auto" max-width="800">
              <v-img
                src="https://cdn.vuetifyjs.com/images/cards/sunshine.jpg"
                height="500px"
              ></v-img>

              <v-card-title>{{ advertisement.nameAd }}</v-card-title>

              <v-divider class="mx-4"></v-divider>

              <v-card-title> {{ advertisement.price }} р </v-card-title>

              <v-card-text>
                <v-btn
                  color="success"
                  dark
                  v-on:click="GetNumber(advertisement)"
                >
                  <v-icon dark> mdi-phone </v-icon>
                  &nbsp;Позвонить
                </v-btn>
                &nbsp; &nbsp;
                <v-btn color="info">
                  <v-icon dark> mdi-message-processing </v-icon>
                  &nbsp;Написать
                </v-btn>
                <p></p>
                <p></p>
                <div class="text--primary">
                  г.{{ advertisement.city }}<br />
                  {{ advertisement.country }}
                </div>
              </v-card-text>

              <v-card-title> Описание </v-card-title>
              <v-divider class="mx-4"></v-divider>
              <v-card-text>
                <div class="text--primary">{{ advertisement.comment }}</div>
              </v-card-text>

              <div>
                <v-card-title v-if="!isResume">Характеристики </v-card-title>
                <v-card-title v-if="isResume">O соискателе </v-card-title>
                <v-divider class="mx-4"></v-divider>
                <v-card-text>
                  <electronics-show
                    v-bind:ad="advertisement"
                    v-show="isElectronicAd"
                  ></electronics-show>
                  <animal-show
                    v-bind:ad="advertisement"
                    v-show="isAnimalAd"
                  ></animal-show>
                  <business-show v-bind:ad="advertisement" v-show="isBusiness">
                  </business-show>
                  <home-and-garden
                    v-bind:ad="advertisement"
                    v-show="isForHomeAndGarden"
                  ></home-and-garden>
                  <hobbies-and-recreation
                    v-bind:ad="advertisement"
                    v-show="isHobbiesAndRecreation"
                  ></hobbies-and-recreation>
                  <personal-items
                    v-bind:ad="advertisement"
                    v-show="isPersonalItems"
                  >
                  </personal-items>
                  <realty v-bind:ad="advertisement" v-show="isRealty"> </realty>
                  <resume v-bind:ad="advertisement" v-show="isResume"></resume>
                  <service
                    v-bind:ad="advertisement"
                    v-show="isService"
                  ></service>
                  <transport
                    v-bind:ad="advertisement"
                    v-show="isTransport"
                  ></transport>
                  <work v-bind:ad="advertisement" v-show="isWork"> </work>
                  <spare-parts-and-acsessories
                    v-bind:ad="advertisement"
                    v-show="isSparePartsAndAcsessories"
                  ></spare-parts-and-acsessories>
                </v-card-text>
              </div>
            </v-card>
          </v-dialog>
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-dialog v-model="contactsDetailDialog" persistent max-width="290">
          <v-card>
            <v-card-title class="text-h5">
              <v-avatar
                class="mr-10"
                color="grey darken-1"
                size="32"
              ></v-avatar>
              {{ ownerAd.userName }}
            </v-card-title>
            <v-card-text>Телефон: {{ ownerAd.phoneNumber }}</v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="contactsDetailDialog = false">
                Закрыть
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import ApiService from "../services/api.service";
import SiteUrl from "../settings/site-url.settings";
import ElectronicsShow from "../components/electronicsShow";
import AnimalShow from "../components/animalShow";
import BusinessShow from "../components/businessShow";
import HomeAndGarden from "../components/homeAndGarden.vue";
import HobbiesAndRecreation from "../components/hobbiesAndRecreation.vue";
import PersonalItems from "../components/personalItems.vue";
import Realty from "../components/realty.vue";
import Resume from "../components/resume.vue";
import Service from "../components/service.vue";
import Transport from "../components/transport.vue";
import Work from "../components/work.vue";
import SparePartsAndAcsessories from "../components/sparePartsAndAcsessories.vue"

export default {

  name: "search",
  props: ["listAd"],
  data: () => ({
    show: false,
    dialog: false,
    advertisement: {},
    isElectronicAd: false,
    isAnimalAd: false,
    isBusiness: false,
    isForHomeAndGarden: false,
    isHobbiesAndRecreation: false,
    isPersonalItems: false,
    isRealty: false,
    isResume: false,
    isService: false,
    isWork: false,
    isSparePartsAndAcsessories: false,
    isTransport: false,
    contactsDetailDialog: false,
    ownerAd: {},
    adCount: [],
    rowCount: 0,
    allAdsCount:0
  }),
  methods: {

    async fetchGetAdvertisement(item) {
      let r = await ApiService.get(
        SiteUrl.getAdById() + "?id=" + item.id,
        false
      );

      if (r.error) alert(r.error);
      else {
        let result = r.data;
        this.advertisement = result;
        this.dialog = true;
        if (this.advertisement.typeAd === "AdByElectronics")
          this.isElectronicAd = true;
        else if (this.advertisement.typeAd === "AdByAnimals")
          this.isAnimalAd = true;
        else if (this.advertisement.typeAd === "AdByBusinessAndEquipment")
          this.isBusiness = true;
        else if (this.advertisement.typeAd === "AdByForHomeAndGarden")
          this.isForHomeAndGarden = true;
        else if (this.advertisement.typeAd === "AdByHobbiesAndRecreation")
          this.isHobbiesAndRecreation = true;
        else if (this.advertisement.typeAd === "AdByPersonalItems")
          this.isPersonalItems = true;
        else if (this.advertisement.typeAd === "AdByRealty")
          this.isRealty = true;
        else if (this.advertisement.typeAd === "AdByResume")
          this.isResume = true;
        else if (this.advertisement.typeAd === "AdByService")
          this.isService = true;
        else if (this.advertisement.typeAd === "AdByTransport")
          this.isTransport = true;
        else if (this.advertisement.typeAd === "AdByWork") this.isWork = true;
        else this.isSparePartsAndAcsessories = true;
      }
    },
    GetNumber(item) {
      this.ownerAd = item.owner;
      this.contactsDetailDialog = true;
    }
  },
  components: {
    ElectronicsShow,
    AnimalShow,
    BusinessShow,
    HomeAndGarden,
    HobbiesAndRecreation,
    PersonalItems,
    Realty,
    Resume,
    Service,
    Transport,
    Work,
    SparePartsAndAcsessories,
  },
};
</script>