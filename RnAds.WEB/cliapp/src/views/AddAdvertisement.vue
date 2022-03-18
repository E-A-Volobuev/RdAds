<template>
  <div>
    <div v-if="email === ''">войдите или зарегистрируйтесь</div>
    <div v-else>
      <v-container>
        <v-row justify="center">
          <v-col cols="auto">
            <v-stepper v-model="e6" vertical width="1000px">
              <v-stepper-step :complete="e6 > 1" step="1">
                Выберите тип объявления
              </v-stepper-step>

              <v-stepper-content step="1">
                <v-form
                  ref="formStepOne"
                  v-model="formStepOneValidate"
                  lazy-validation
                >
                  <v-select
                    v-model="typeAd"
                    :items="types"
                    :rules="[(v) => !!v || 'Не выбран тип объявления']"
                    label="Тип объявления"
                    required
                  ></v-select>
                  <p></p>
                  <p></p>
                  <v-btn
                    color="primary"
                    @click="e6 = 2"
                    :disabled="!formStepOneValidate"
                  >
                    Далее
                  </v-btn>
                </v-form>
              </v-stepper-content>

              <v-stepper-step :complete="e6 > 2" step="2">
                Описание объявления
              </v-stepper-step>

              <v-stepper-content step="2">
                <v-form
                  ref="formStepTwo"
                  v-model="formStepTwoValidate"
                  lazy-validation
                >
                  <v-text-field
                    dense
                    v-model="nameAd"
                    label="Название объявления"
                    :rules="[(v) => !!v || 'Укажите название объявления']"
                    required
                  ></v-text-field>
                  <ad-animal
                    v-bind:ad="advertisement"
                    v-if="isAnimalAd"
                  ></ad-animal>
                  <business-ad
                    v-bind:ad="advertisement"
                    v-bind:conditions="conditions"
                    v-if="isBusiness"
                  ></business-ad>
                  <ad-electronic
                    v-bind:ad="advertisement"
                    v-bind:conditions="conditions"
                    v-bind:availabilities="availabilities"
                    v-if="isElectronicAd"
                  ></ad-electronic>
                  <ad-home-and-garden
                    v-bind:ad="advertisement"
                    v-bind:conditions="conditions"
                    v-if="isForHomeAndGarden"
                  ></ad-home-and-garden>
                  <ad-home-and-garden
                    v-bind:ad="advertisement"
                    v-if="isHobbiesAndRecreation"
                  ></ad-home-and-garden>
                  <ad-home-and-garden
                    v-bind:ad="advertisement"
                    v-if="isPersonalItems"
                  ></ad-home-and-garden>
                  <ad-realty
                    v-bind:ad="advertisement"
                    v-bind:conditions="conditions"
                    v-if="isRealty"
                  ></ad-realty>
                  <ad-resume
                    v-bind:ad="advertisement"
                    v-bind:sex="sex"
                    v-if="isResume"
                  ></ad-resume>
                  <ad-service
                    v-bind:ad="advertisement"
                    v-if="isService"
                  ></ad-service>
                  <ad-spare-parts
                    v-bind:ad="advertisement"
                    v-bind:conditions="conditions"
                    v-bind:availabilities="availabilities"
                    v-if="isSparePartsAndAcsessories"
                  ></ad-spare-parts>
                  <ad-transport
                    v-bind:ad="advertisement"
                    v-if="isTransport"
                  ></ad-transport>
                  <ad-work
                    v-bind:ad="advertisement"
                    v-bind:sex="sex"
                    v-if="isWork"
                  ></ad-work>
                  <v-textarea
                    outlined
                    v-model="commentAd"
                    label="Описание объявления"
                    :rules="[(v) => !!v || 'Укажите описание объявления']"
                    required
                  ></v-textarea>
                  <v-text-field
                    label="Цена"
                    v-model="priceAd"
                    filled
                    type="number"
                    rounded
                    dense
                    :rules="[(v) => !!v || 'Укажите цену']"
                    required
                  ></v-text-field>

                  <v-combobox
                    label="Страна"
                    v-model="countryAd"
                    :items="countries"
                    outlined
                    dense
                    :rules="[(v) => !!v || 'Укажите страну']"
                    required
                  ></v-combobox>
                  <v-combobox
                    label="Город"
                    v-model="cityAd"
                    :items="regions"
                    outlined
                    dense
                    :rules="[(v) => !!v || 'Укажите город']"
                    required
                  ></v-combobox>

                  <v-btn
                    color="primary"
                    @click="e6 = 3"
                    :disabled="!formStepTwoValidate"
                  >
                    Далее
                  </v-btn>
                  <v-btn text @click="e6 = 1"> Назад </v-btn>
                </v-form>
              </v-stepper-content>

              <v-stepper-step :complete="e6 > 3" step="3">
                Добавление фотографий
              </v-stepper-step>

              <v-stepper-content step="3">
                <v-row>
                  <v-col
                    ><v-file-input
                      dense
                      outlined
                      label="Выберите первое фото"
                      v-model="firstPhoto"
                      accept=".jpg, .png"
                      hide-details
                      class="mx-2"
                      @change="onFileChange(firstPhoto, 1)"
                    ></v-file-input>
                  </v-col>
                  <v-col>
                    <v-file-input
                      dense
                      outlined
                      label="Выберите второе фото"
                      v-model="secondPhoto"
                      accept=".jpg, .png"
                      hide-details
                      class="mx-2"
                      @change="onFileChange(secondPhoto, 2)"
                    ></v-file-input
                  ></v-col>
                </v-row>
                <v-row>
                  <v-col></v-col>
                  <v-col
                    ><img
                      v-if="urlFirst"
                      :src="urlFirst"
                      width="200"
                      height="150"
                  /></v-col>
                  <v-col></v-col>
                  <v-col>
                    <v-col
                      ><img
                        v-if="urlSecond"
                        :src="urlSecond"
                        width="200"
                        height="150"
                    /></v-col>
                  </v-col>
                  <v-col></v-col>
                </v-row>
                <v-row>
                  <v-col
                    ><v-file-input
                      dense
                      outlined
                      label="Выберите третье фото"
                      v-model="thirdPhoto"
                      accept=".jpg, .png"
                      hide-details
                      class="mx-2"
                      @change="onFileChange(thirdPhoto, 3)"
                    ></v-file-input
                  ></v-col>
                  <v-col
                    ><v-file-input
                      dense
                      outlined
                      label="Выберите четвёртое фото"
                      v-model="fourthPhoto"
                      accept=".jpg, .png"
                      hide-details
                      class="mx-2"
                      @change="onFileChange(fourthPhoto, 4)"
                    ></v-file-input
                  ></v-col>
                </v-row>
                <v-row>
                  <v-col></v-col>
                  <v-col
                    ><img
                      v-if="urlThird"
                      :src="urlThird"
                      width="200"
                      height="150"
                  /></v-col>
                  <v-col></v-col>
                  <v-col>
                    <v-col
                      ><img
                        v-if="urlFourth"
                        :src="urlFourth"
                        width="200"
                        height="150"
                    /></v-col>
                  </v-col>
                  <v-col></v-col>
                </v-row>
                <v-row>
                  <v-col
                    ><v-file-input
                      dense
                      outlined
                      label="Выберите пятое фото"
                      v-model="fifthPhoto"
                      accept=".jpg, .png"
                      hide-details
                      class="mx-2"
                      @change="onFileChange(fifthPhoto, 5)"
                    ></v-file-input
                  ></v-col>
                  <v-col
                    ><v-file-input
                      dense
                      outlined
                      label="Выберите шестое фото"
                      v-model="sixthPhoto"
                      accept=".jpg, .png"
                      hide-details
                      class="mx-2"
                      @change="onFileChange(sixthPhoto, 6)"
                    ></v-file-input>
                  </v-col>
                </v-row>
                <v-row>
                  <v-col></v-col>
                  <v-col
                    ><img
                      v-if="urlFifth"
                      :src="urlFifth"
                      width="200"
                      height="150"
                  /></v-col>
                  <v-col></v-col>
                  <v-col>
                    <v-col
                      ><img
                        v-if="urlSixth"
                        :src="urlSixth"
                        width="200"
                        height="150"
                    /></v-col>
                  </v-col>
                  <v-col></v-col>
                </v-row>
                <v-row>
                  <v-col
                    ><v-btn
                      color="primary"
                      dark
                      @click="fetchSendObject"
                      :loading="loading"
                    >
                      Готово
                      <v-icon right dark> mdi-cloud-upload </v-icon>
                    </v-btn>
                    <v-btn text @click="e6 = 2"> Шаг назад</v-btn>
                  </v-col>
                  <v-col></v-col>
                  <v-col></v-col>
                </v-row>
                <v-row> </v-row>
              </v-stepper-content>
            </v-stepper>
          </v-col>
        </v-row>
      </v-container>
    </div>
  </div>
</template>

<script>
import adAnimal from "../components/Added/adAnimal.vue";
import businessAd from "../components/Added/addBusiness.vue";
import AdElectronic from "../components/Added/adElectronic.vue";
import adHomeAndGarden from "../components/Added/adHomeAndGarden.vue";
import adRealty from "../components/Added/adRealty.vue";
import AdResume from "../components/Added/adResume.vue";
import AdService from "../components/Added/adService.vue";
import AdSpareParts from "../components/Added/adSpareParts.vue";
import AdTransport from "../components/Added/adTransport.vue";
import AdWork from "../components/Added/adWork.vue";
import ApiService from "../services/api.service";
import SiteUrl from "../settings/site-url.settings";

export default {
  components: {
    adAnimal,
    businessAd,
    AdElectronic,
    adHomeAndGarden,
    adRealty,
    AdResume,
    AdService,
    AdSpareParts,
    AdTransport,
    AdWork,
  },
  mounted: function () {
    this.validFunc();
    this.validFuncTwo();
  },
  computed: {
    email: function () {
      return this.$store.getters.getEmail;
    },
  },
  data: () => ({
    e6: 1,
    types: [
      "Животные",
      "Бизнес и оборудование для бизнеса",
      "Электроника",
      "Для дома и дачи",
      "Хобби и отдых",
      "Личные вещи",
      "Недвижимость",
      "Резюме",
      "Услуги",
      "Транспорт",
      "Работа",
      "Запасные части и аксессуары",
    ],
    conditions: ["новое", "б/у"],
    sex: ["Муж", "Жен"],
    availabilities: ["в наличии", "под заказ"],
    countries: ["РФ"],
    regions: [],
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
    typeAd: "",
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
    formStepOneValidate: false,
    formStepTwoValidate: false,
    loading: false,
    nameAd: "",
    commentAd: "",
    priceAd: 0,
    cityAd: "",
    countryAd: "",
  }),
  watch: {
    typeAd: function (setType) {
      this.SetFalseValue();
      if (setType === "Электроника") this.isElectronicAd = true;
      else if (setType === "Животные") this.isAnimalAd = true;
      else if (setType === "Бизнес и оборудование для бизнеса")
        this.isBusiness = true;
      else if (setType === "Для дома и дачи") this.isForHomeAndGarden = true;
      else if (setType === "Хобби и отдых") this.isHobbiesAndRecreation = true;
      else if (setType === "Личные вещи") this.isPersonalItems = true;
      else if (setType === "Недвижимость") this.isRealty = true;
      else if (setType === "Резюме") this.isResume = true;
      else if (setType === "Сервис") this.isService = true;
      else if (setType === "Транспорт") this.isTransport = true;
      else if (setType === "Работа") this.isWork = true;
      else if (setType === "Запасные части и аксессуары")
        this.isSparePartsAndAcsessories = true;
    },
  },
  created() {
    this.fetchGetNamesRegion();
  },
  methods: {
    SetFalseValue() {
      this.isElectronicAd = false;
      this.isAnimalAd = false;
      this.isBusiness = false;
      this.isForHomeAndGarden = false;
      this.isHobbiesAndRecreation = false;
      this.isPersonalItems = false;
      this.isRealty = false;
      this.isResume = false;
      this.isService = false;
      this.isWork = false;
      this.isSparePartsAndAcsessories = false;
      this.isTransport = false;
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
    validFunc() {
      if (this.$refs.formStepOne) this.$refs.formStepOne.validate();
    },
    validFuncTwo() {
      if (this.$refs.formStepTwo) this.$refs.formStepTwo.validate();
    },
    async fetchSendObject() {
      const token = sessionStorage.getItem("accessToken");
      const r = await fetch(SiteUrl.createAd(), {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },

        body: JSON.stringify({
          NameAd: this.nameAd,
          Country: this.countryAd,
          City: this.cityAd,
          Comment: this.commentAd,
          Price: this.priceAd,
          TypeAd: this.typeAd,
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
        await this.fetchAddPhoto(id);
      } else {
        alert("ошибка сервера");
      }
    },
    async fetchAddPhoto(id) {
      var photo = [];
      photo.push(this.firstPhoto);
      photo.push(this.secondPhoto);
      photo.push(this.thirdPhoto);
      photo.push(this.fourthPhoto);
      photo.push(this.fifthPhoto);
      photo.push(this.sixthPhoto);

      let formData = new FormData();
      formData.append("id", id);
      Array.from(photo).forEach((file) => {
        formData.append("photo", file);
      });

      const response = await fetch(SiteUrl.createPhoto(), {
        method: "POST",
        body: formData,
      });

      if (response.ok === true) {
        this.loading = false;
        this.$router.push(`/`);
      } else {
        alert("ошибка создания объявления");
      }
    },
    async fetchGetNamesRegion() {
      let r = await ApiService.get(SiteUrl.getNamesRegion(), false);

      if (r.error) alert(r.error);
      else {
        let items = r.data;
        this.regions = items;
      }
    },
  },
};
</script>