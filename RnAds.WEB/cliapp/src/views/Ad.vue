<template>
  <div>
    <v-container>
      <v-row justify="center">
        <v-col cols="auto">
          <v-card class="mx-auto" max-width="800">
            <v-carousel>
              <v-carousel-item
                v-for="pic in pictures"
                :key="pic.id"
                :src="pic.src"
                reverse-transition="fade-transition"
                transition="fade-transition"
              ></v-carousel-item>
            </v-carousel>

            <v-card-title>{{ advertisement.nameAd }}</v-card-title>

            <v-divider class="mx-4"></v-divider>

            <v-card-title> {{ advertisement.price }} р </v-card-title>

            <v-card-text>
              <v-btn
                color="success"
                dark
                v-on:click="fetchGetContact(advertisement.owner.id)"
              >
                <v-icon dark> mdi-phone </v-icon>
                &nbsp;Позвонить
              </v-btn>
              &nbsp; &nbsp;
              <v-btn color="info" @click="WriteMessage">
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
                  v-if="isElectronicAd"
                ></electronics-show>
                <animal-show
                  v-bind:ad="advertisement"
                  v-if="isAnimalAd"
                ></animal-show>
                <business-show v-bind:ad="advertisement" v-if="isBusiness">
                </business-show>
                <home-and-garden
                  v-bind:ad="advertisement"
                  v-if="isForHomeAndGarden"
                ></home-and-garden>
                <home-and-garden
                  v-bind:ad="advertisement"
                  v-if="isHobbiesAndRecreation"
                ></home-and-garden>
                <home-and-garden
                  v-bind:ad="advertisement"
                  v-if="isPersonalItems"
                >
                </home-and-garden>
                <realty v-bind:ad="advertisement" v-if="isRealty"> </realty>
                <resume v-bind:ad="advertisement" v-if="isResume"></resume>
                <service v-bind:ad="advertisement" v-if="isService"></service>
                <transport
                  v-bind:ad="advertisement"
                  v-if="isTransport"
                ></transport>
                <work v-bind:ad="advertisement" v-if="isWork"> </work>
                <spare-parts-and-acsessories
                  v-bind:ad="advertisement"
                  v-if="isSparePartsAndAcsessories"
                ></spare-parts-and-acsessories>
              </v-card-text>
            </div>
          </v-card>
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-dialog v-model="contactsDetailDialog" persistent max-width="290">
          <v-card>
            <v-toolbar color="primary" dark height="90">
              <v-avatar class="mb-4" color="grey darken-1" size="64">
                <img :src="avatar" width="40" height="40" /></v-avatar
              >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <v-toolbar-title class="text-h5">{{
                advertisement.owner.name
              }}</v-toolbar-title>
            </v-toolbar>
            <v-divider class="mx-4"></v-divider>
            <v-list-item>
              <v-list-item-title class="text-h6">
                <v-icon> mdi-phone-forward </v-icon
                >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                {{ advertisement.owner.phone }}
              </v-list-item-title>
            </v-list-item>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="contactsDetailDialog = false">
                Закрыть
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-row>
      <v-row justify="space-around">
        <v-col cols="auto">
          <v-dialog
            transition="dialog-top-transition"
            max-width="600"
            v-model="dialog"
          >
            <v-card>
              <v-toolbar color="primary" dark>
                <v-icon dark> mdi-email-edit </v-icon>
                &nbsp; Новое сообщение
              </v-toolbar>
              <p></p>
              <p></p>
              <p></p>
              <v-card-text>
                <v-textarea outlined label="Filled" v-model="message">
                  <template slot="label">
                    <v-icon>mdi-playlist-edit</v-icon> Введите текст
                  </template></v-textarea
                >
              </v-card-text>
              <v-card-actions class="justify-end">
                <v-btn text color="primary" @click="fetchSendMessage"
                  >Отправить</v-btn
                >
                <v-spacer></v-spacer>
                <v-btn text @click="dialog = false">Закрыть</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-col>
      </v-row>
      <v-row justify="center">
        <v-col cols="auto">
          <v-alert
            v-model="alertMessage"
            dismissible
            type="success"
            elevation="2"
            style="width: 800px"
          >
            Сообщение отправлено!
          </v-alert>
        </v-col>
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
import Realty from "../components/realty.vue";
import Resume from "../components/resume.vue";
import Service from "../components/service.vue";
import Transport from "../components/transport.vue";
import Work from "../components/work.vue";
import SparePartsAndAcsessories from "../components/sparePartsAndAcsessories.vue";

export default {
  props: {
    id: {
      type: String,
      default: null,
    },
  },
  created() {
    this.fetchGetAdvertisement(this.id);
  },
  computed: {
    pictures() {
      if (!this.advertisement.pictures) return [];

      return this.advertisement.pictures.map((e) => {
        return {
          id: e.id,
          src: `${SiteUrl.getPicture()}${e.id}`,
        };
      });
    },
    email: function () {
      return this.$store.getters.getEmail;
    },
  },
  data: () => ({
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
    dialog: false,
    avatar: null,
    message: "",
    alertMessage: false
  }),
  methods: {
    async fetchGetAdvertisement(id) {
      let r = await ApiService.get(SiteUrl.getAdById() + "?id=" + id, false);

      if (r.error) alert(r.error);
      else {
        let result = r.data;
        this.advertisement = result;
        if (this.advertisement.typeAd === "Электроника")
          this.isElectronicAd = true;
        else if (this.advertisement.typeAd === "Животные")
          this.isAnimalAd = true;
        else if (
          this.advertisement.typeAd === "Бизнес и оборудование для бизнеса"
        )
          this.isBusiness = true;
        else if (this.advertisement.typeAd === "Для дома и дачи")
          this.isForHomeAndGarden = true;
        else if (this.advertisement.typeAd === "Хобби и отдых")
          this.isHobbiesAndRecreation = true;
        else if (this.advertisement.typeAd === "Личные вещи")
          this.isPersonalItems = true;
        else if (this.advertisement.typeAd === "Недвижимость")
          this.isRealty = true;
        else if (this.advertisement.typeAd === "Резюме") this.isResume = true;
        else if (this.advertisement.typeAd === "Сервис") this.isService = true;
        else if (this.advertisement.typeAd === "Транспорт")
          this.isTransport = true;
        else if (this.advertisement.typeAd === "Работа") this.isWork = true;
        else this.isSparePartsAndAcsessories = true;
      }
    },
    async fetchGetContact(id) {
      const response = await fetch(SiteUrl.getAvatarOwner() + id, {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      });
      if (response.ok === true) {
        let r = await response.blob();
        this.avatar = URL.createObjectURL(r);
        this.contactsDetailDialog = true;
      } else console.log("Status: ", response.status);
    },
    WriteMessage() {
      if (this.email === "") alert("Войдите или зарегистрируйтесь");
      else this.dialog = true;
    },
    async fetchSendMessage() {
      const token = sessionStorage.getItem("accessToken");
      const r = await fetch(SiteUrl.writeMessage(), {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },

        body: JSON.stringify({
          ToUserId: this.advertisement.owner.id,
          Text: this.message,
        }),
      });

      if (r.ok === true) {
        this.dialog = false;
        this.alertMessage = true;
      } else {
        alert("ошибка отправки сообщения");
      }
    },
  },
  components: {
    ElectronicsShow,
    AnimalShow,
    BusinessShow,
    HomeAndGarden,
    Realty,
    Resume,
    Service,
    Transport,
    Work,
    SparePartsAndAcsessories,
  },
};
</script>