<template>
  <div>
    <v-container>
      <v-row justify="center">
        <v-col cols="auto">
          <v-card
            class="mx-auto"
            max-width="1000"
            min-width="700"
            elevation="12"
          >
            <v-row>
              <v-col v-for="(pic, index) in pictures" :key="pic.id" md="4">
                <v-btn icon large color="red" v-on:click="DeletePhoto(index)">
                  <v-icon>mdi-close-circle</v-icon>
                </v-btn>
                <v-img :src="pic.src" max-width="200" max-height="150"></v-img>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <div v-if="firstShow">
                  <v-file-input
                    dense
                    outlined
                    label="Выберите фото"
                    v-model="firstPhoto"
                    accept=".jpg, .png"
                    hide-details
                    class="mx-2"
                    @change="onFileChange(firstPhoto, 1)"
                  ></v-file-input>
                  <p></p>
                  <p>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                      v-if="urlFirst"
                      :src="urlFirst"
                      width="200"
                      height="150"
                    />
                  </p>
                </div>
              </v-col>
              <v-col>
                <div v-if="secondShow">
                  <v-file-input
                    dense
                    outlined
                    label="Выберите фото"
                    v-model="secondPhoto"
                    accept=".jpg, .png"
                    hide-details
                    class="mx-2"
                    @change="onFileChange(secondPhoto, 2)"
                  ></v-file-input>
                  <p></p>
                  <p>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                      v-if="urlSecond"
                      :src="urlSecond"
                      width="200"
                      height="150"
                    />
                  </p>
                </div>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <div v-if="thirdShow">
                  <v-file-input
                    dense
                    outlined
                    label="Выберите фото"
                    v-model="thirdPhoto"
                    accept=".jpg, .png"
                    hide-details
                    class="mx-2"
                    @change="onFileChange(thirdPhoto, 3)"
                  ></v-file-input>
                  <p></p>
                  <p>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                      v-if="urlThird"
                      :src="urlThird"
                      width="200"
                      height="150"
                    />
                  </p>
                </div>
              </v-col>
              <v-col>
                <div v-if="fourthShow">
                  <v-file-input
                    dense
                    outlined
                    label="Выберите фото"
                    v-model="fourthPhoto"
                    accept=".jpg, .png"
                    hide-details
                    class="mx-2"
                    @change="onFileChange(fourthPhoto, 4)"
                  ></v-file-input>
                  <p></p>
                  <p>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                      v-if="urlFourth"
                      :src="urlFourth"
                      width="200"
                      height="150"
                    />
                  </p>
                </div>
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <div v-if="fifthShow">
                  <v-file-input
                    dense
                    outlined
                    label="Выберите фото"
                    v-model="fifthPhoto"
                    accept=".jpg, .png"
                    hide-details
                    class="mx-2"
                    @change="onFileChange(fifthPhoto, 5)"
                  ></v-file-input>
                  <p></p>
                  <p>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                      v-if="urlFifth"
                      :src="urlFifth"
                      width="200"
                      height="150"
                    />
                  </p>
                </div>
              </v-col>
              <v-col>
                <div v-if="sixthShow">
                  <v-file-input
                    dense
                    outlined
                    label="Выберите фото"
                    v-model="sixthPhoto"
                    accept=".jpg, .png"
                    hide-details
                    class="mx-2"
                    @change="onFileChange(sixthPhoto, 6)"
                  ></v-file-input>
                  <p></p>
                  <p>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img
                      v-if="urlSixth"
                      :src="urlSixth"
                      width="200"
                      height="150"
                    />
                  </p>
                </div>
              </v-col>
            </v-row>
            <v-row justify="center">
              <v-col cols="auto"
                ><v-btn color="primary" v-on:click="AddPhoto">
                  Добавить фото
                  <v-icon right dark> mdi-camera </v-icon>
                </v-btn>
              </v-col>
            </v-row>

            <v-card-title>
              <v-row>
                <v-col md="2" align-self="center">
                  <v-icon x-large> mdi-playlist-edit </v-icon>
                </v-col>
                <v-col md="8">
                  <v-text-field
                    v-model="advertisement.nameAd"
                    label="Название объявления"
                    outlined
                  ></v-text-field>
                </v-col>
                <v-col md="2"></v-col>
              </v-row>
            </v-card-title>

            <v-divider class="mx-4"></v-divider>

            <v-card-title>
              <v-row>
                <v-col md="3" align-self="center">
                  <v-icon x-large> mdi-cash-multiple </v-icon>
                </v-col>
                <v-col md="5">
                  <v-text-field
                    v-model="advertisement.price"
                    label="Цена"
                    outlined
                  ></v-text-field>
                </v-col>
                <v-col md="2" align-self="center"
                  ><p class="text-h4">&#8381;</p></v-col
                >
                <v-col md="2"></v-col>
              </v-row>
            </v-card-title>

            <v-card-text>
              <v-row>
                <v-col md="3" align-self="center">
                  <v-icon x-large> mdi-home-city </v-icon>
                </v-col>
                <v-col md="5">
                  <v-autocomplete
                    v-model="advertisement.city"
                    :items="regions"
                    dense
                    outlined
                    label="Выберите город или регион"
                  ></v-autocomplete>
                </v-col>
                <v-col md="4"></v-col>
              </v-row>
              <v-row>
                <v-col md="3" align-self="center">
                  <v-icon x-large> mdi-map </v-icon>
                </v-col>
                <v-col md="5">
                  <v-text-field
                    v-model="advertisement.country"
                    label="Страна"
                    outlined
                  ></v-text-field>
                </v-col>
                <v-col md="4"></v-col>
              </v-row>
            </v-card-text>

            <v-card-title> Описание </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text>
              <v-textarea filled v-model="advertisement.comment"></v-textarea>
            </v-card-text>

            <div>
              <v-card-title v-if="!isResume">Характеристики </v-card-title>
              <v-card-title v-if="isResume">O соискателе </v-card-title>
              <v-divider class="mx-4"></v-divider>
              <v-card-text>
                <electronics-show
                  v-bind:ad="advertisement"
                  isEdit="true"
                  v-bind:conditions="conditions"
                  v-bind:availabilities="availabilities"
                  v-if="isElectronicAd"
                ></electronics-show>
                <animal-show
                  v-bind:ad="advertisement"
                  isEdit="true"
                  v-if="isAnimalAd"
                ></animal-show>
                <business-show
                  v-bind:ad="advertisement"
                  v-if="isBusiness"
                  isEdit="true"
                  v-bind:conditions="conditions"
                >
                </business-show>
                <home-and-garden
                  v-bind:ad="advertisement"
                  isEdit="true"
                  v-bind:conditions="conditions"
                  v-if="isForHomeAndGarden"
                ></home-and-garden>
                <home-and-garden
                  v-bind:ad="advertisement"
                  isEdit="true"
                  v-bind:conditions="conditions"
                  v-if="isHobbiesAndRecreation"
                ></home-and-garden>
                <home-and-garden
                  v-bind:ad="advertisement"
                  isEdit="true"
                  v-bind:conditions="conditions"
                  v-if="isPersonalItems"
                >
                </home-and-garden>
                <realty
                  v-bind:ad="advertisement"
                  v-if="isRealty"
                  isEdit="true"
                  v-bind:conditions="conditions"
                >
                </realty>
                <resume
                  v-bind:ad="advertisement"
                  v-if="isResume"
                  isEdit="true"
                  v-bind:sex="sex"
                ></resume>
                <service
                  v-bind:ad="advertisement"
                  v-if="isService"
                  isEdit="true"
                ></service>
                <transport
                  v-bind:ad="advertisement"
                  isEdit="true"
                  v-if="isTransport"
                ></transport>
                <work
                  v-bind:ad="advertisement"
                  v-if="isWork"
                  v-bind:sex="sex"
                  isEdit="true"
                >
                </work>
                <spare-parts-and-acsessories
                  v-bind:ad="advertisement"
                  v-if="isSparePartsAndAcsessories"
                  isEdit="true"
                  v-bind:conditions="conditions"
                  v-bind:availabilities="availabilities"
                ></spare-parts-and-acsessories>
              </v-card-text>
              <v-card-actions class="pt-0">
                <v-btn text color="success" @click="fetchSendObject" :loading="loading">
                  Сохранить
                </v-btn>
                <v-spacer></v-spacer>
                <v-btn text color="primary" :to="`/MyAds`"> Закрыть </v-btn>
              </v-card-actions>
            </div>
          </v-card>
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
    this.fetchGetNamesRegion();
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
    avatar: null,
    regions: [],
    conditions: ["новое", "б/у"],
    sex: ["Муж", "Жен"],
    availabilities: ["в наличии", "под заказ"],
    pictures: [],
    counter: 0,
    firstPhoto: null,
    secondPhoto: null,
    thirdPhoto: null,
    fourthPhoto: null,
    fifthPhoto: null,
    sixthPhoto: null,
    urlFirst: null,
    urlSecond: null,
    urlThird: null,
    urlFourth: null,
    urlFifth: null,
    urlSixth: null,
    firstShow: false,
    secondShow: false,
    thirdShow: false,
    fourthShow: false,
    fifthShow: false,
    sixthShow: false,
    loading:false,
    currentPicture:[]
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

        this.pictures = this.advertisement.pictures.map((e) => {
          return {
            id: e.id,
            src: `${SiteUrl.getPicture()}${e.id}`,
          };
        });
        this.counter = this.pictures.length;
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
    async fetchGetNamesRegion() {
      let r = await ApiService.get(SiteUrl.getNamesRegion(), false);

      if (r.error) alert(r.error);
      else {
        let items = r.data;
        this.regions = items;
      }
    },
    async fetchSendObject() {
      const token = sessionStorage.getItem("accessToken");
      const r = await fetch(SiteUrl.EditAd(), {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },

        body: JSON.stringify({
          Id: this.advertisement.id,
          NameAd: this.advertisement.nameAd,
          Country: this.advertisement.country,
          City: this.advertisement.city,
          Comment: this.advertisement.comment,
          Price: this.advertisement.price,
          TypeAd: this.advertisement.typeAd,
          TypeItem: this.advertisement.typeItem,
          CategoryItem: this.advertisement.categoryItem,
          Condition: this.advertisement.сondition,
          Availability: this.advertisement.availability,
          Brand: this.advertisement.brand,
          Area: this.advertisement.area,
          NumberFloor: this.advertisement.numberFloor,
          CountRooms: this.advertisement.countRooms,
          Citizenship: this.advertisement.citizenship,
          SphereOfActivity: this.advertisement.sphereOfActivity,
          WorkingSchedule: this.advertisement.workingSchedule,
          Sex: this.advertisement.sex,
          WorkExperience: this.advertisement.workExperience,
          FrequencyOfPayments: this.advertisement.frequencyOfPayments,
          YearReliase: this.advertisement.yearReliase,
          Kilometers: this.advertisement.kilometers,
          OwnerCount: this.advertisement.ownerCount,
          EngineVolume: this.advertisement.engineVolume,
          HorsePower: this.advertisement.horsePower,
          TypeEngine: this.advertisement.typeEngine,
          TypeGearBox: this.advertisement.typeGearBox,
          TypeDrive: this.advertisement.typeDrive,
          TypeBody: this.advertisement.typeBody,
          Color: this.advertisement.color,
          TypeHelm: this.advertisement.typeHelm,
          TypeSparePartsAndAcsessories:
            this.advertisement.typeSparePartsAndAcsessories,
        }),
      });

      if (r.ok === true) {
        let id = await r.json();
        await this.fetchSendPhoto(id);
      } else {
        alert("ошибка сервера");
      }
    },
    AddPhoto() {
      if (this.counter < 6) {
        if (this.counter === 0) this.firstShow = true;
        if (this.counter === 1) this.secondShow = true;
        if (this.counter === 2) this.thirdShow = true;
        if (this.counter === 3) this.fourthShow = true;
        if (this.counter === 4) this.fifthShow = true;
        if (this.counter === 5) this.sixthShow = true;
        this.counter = this.counter + 1;
      } else alert("объявление может содержать не более 6 фотографий");
    },
    DeletePhoto(index) {
      this.pictures.splice(index, 1);
      this.counter = this.counter - 1;
    },
    onFileChange(file, number) {
      if (file === null) {
        if (number === 1) this.urlFirst = null;
        if (number === 2) this.urlSecond = null;
        if (number === 3) this.urlThird = null;
        if (number === 4) this.urlFourth = null;
        if (number === 5) this.urlFifth = null;
        if (number === 6) this.urlSixth = null;
      } else {
        if (number === 1) this.urlFirst = URL.createObjectURL(file);
        if (number === 2) this.urlSecond = URL.createObjectURL(file);
        if (number === 3) this.urlThird = URL.createObjectURL(file);
        if (number === 4) this.urlFourth = URL.createObjectURL(file);
        if (number === 5) this.urlFifth = URL.createObjectURL(file);
        if (number === 6) this.urlSixth = URL.createObjectURL(file);
      }
    },
    async fetchSendPhoto(id) {
      var photo = [];
      photo.push(this.firstPhoto);
      photo.push(this.secondPhoto);
      photo.push(this.thirdPhoto);
      photo.push(this.fourthPhoto);
      photo.push(this.fifthPhoto);
      photo.push(this.sixthPhoto);

      this.pictures.forEach((obj)=>{ this.currentPicture.push(obj.id)});

      let formData = new FormData();
      formData.append("id", id);
      formData.append("picturesId", this.currentPicture);
      Array.from(photo).forEach((file) => {
        formData.append("photo", file);
      });

      const response = await fetch(SiteUrl.updatePhoto(), {
        method: "PUT",
        body: formData,
      });

      if (response.ok === true) {
        this.loading = false;
        setTimeout(() => {
         this.$router.push({ path: "/MyAds" });
       }, 500);
      } else {
        alert("ошибка создания фотографий");
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