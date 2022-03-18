<template>
  <div v-if="email === ''">войдите или зарегистрируйтесь</div>
  <div v-else>
    <v-container>
      <v-row>
        <v-col md="4">
          <v-list rounded>
            <v-subheader>Диалоги:</v-subheader>
            <v-list-item-group v-model="selectedItem" color="primary">
              <v-list-item
                v-for="(item, i) in results"
                :key="i"
                @click="menuActionClick(item)"
              >
                <v-list-item-content>
                  <v-list-item-title v-text="item.nameUser"></v-list-item-title>
                  <v-list-item-subtitle
                    v-text="item.textMessage"
                  ></v-list-item-subtitle>
                  <v-spacer></v-spacer>
                  <v-list-item-subtitle
                    v-text="item.time"
                  ></v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-col>
        <v-col md="5">
          <v-card elevation="8" v-if="selectDialod">
            <v-list-item v-for="(item, i) in selectDialod.messages" :key="i">
              <v-list-item-content>
                <v-list-item-title
                  v-text="item.senderUserName"
                ></v-list-item-title>
                <v-list-item-subtitle v-text="item.text"></v-list-item-subtitle>
                <v-container>
                  <v-row>
                    <v-col v-for="picId in item.picturesId" :key="picId" md="4">
                        <v-img
                          :src="getUrlPicture(picId)"
                          contain
                          max-height="300"
                          @click="OpenPicture(picId)"
                        ></v-img>
                    </v-col>
                  </v-row>
                </v-container>
                <v-spacer></v-spacer>
                <v-list-item-subtitle v-text="item.time"></v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
            <v-card-actions>
              <v-row>
                <v-col md="9">
                  <v-textarea
                    outlined
                    label="Напишите что-нибудь.."
                    height="100"
                    v-model="message"
                  ></v-textarea>
                </v-col>
                <v-col md="3">
                  <v-btn
                    dark
                    color="primary"
                    @click="fetchSendMessage(selectDialod)"
                  >
                    <v-icon dark large> mdi-email-send </v-icon>
                  </v-btn>
                  <p></p>
                  <p>
                    <v-btn icon @click="firstShow = !firstShow">
                      <v-icon large>mdi-camera-plus</v-icon>
                    </v-btn>
                  </p>
                </v-col>
              </v-row>
            </v-card-actions>
            <div v-if="firstShow">
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
            </div>
          </v-card>
        </v-col>
      </v-row>
      <v-row>
        <v-dialog v-model="dialog" max-width="1800">
          <v-img :src="openPicture" contain></v-img>
        </v-dialog>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import SiteUrl from "../settings/site-url.settings";
import * as signalR from "@microsoft/signalr";

export default {
  data: () => ({
    results: [],
    selectedItem: 1,
    selectDialod: null,
    message: "",
    userTo: "",
    connection: null,
    firstShow: false,
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
    openPicture: "",
    dialog:false
  }),
  computed: {
    email: function () {
      return this.$store.getters.getEmail;
    },
    mes: function () {
      return this.$store.getters.getMessage;
    },
  },
  created() {
    this.fetchAllChatsNames();
  },
  mounted: function () {
    this.HubConnection();
  },
  methods: {
    async fetchAllChatsNames() {
      const token = sessionStorage.getItem("accessToken");
      const response = await fetch(SiteUrl.getAllNamesChats(), {
        method: "GET",
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + token,
        },
      });
      if (response.ok === true) {
        let data = await response.json();
        this.results = data;
      } else console.log("Status: ", response.status);
    },
    menuActionClick(action) {
      this.selectDialod = action;
    },
    HubConnection() {
      var token = sessionStorage.getItem("accessToken");
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:44387/chat", {
          accessTokenFactory: () => token,
        })
        .build();

      this.connection.on("Receive", (message) => {
        this.$store.commit("setMessage", message);
        setTimeout(() => {
          this.AddMessageToDialog();
        }, 1000);
      });

      this.connection.start();
    },
    async fetchSendMessage(item) {
      var photo = [];
      photo.push(this.firstPhoto);
      photo.push(this.secondPhoto);
      photo.push(this.thirdPhoto);
      photo.push(this.fourthPhoto);
      photo.push(this.fifthPhoto);
      photo.push(this.sixthPhoto);

      let formData = new FormData();
      formData.append("toUserName", item.loginUser);
      formData.append("text", this.message);

      Array.from(photo).forEach((file) => {
        formData.append("photo", file);
      });

      const token = sessionStorage.getItem("accessToken");
      const r = await fetch(SiteUrl.createChatsMessage(), {
        method: "POST",
        headers: {
          Authorization: "Bearer " + token,
        },
        body: formData,
      });

      if (r.ok === true) {
        this.message="";
      } else {
        alert("ошибка сервера");
      }
    },
    AddMessageToDialog() {
      this.selectDialod.messages.push(this.mes);
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
    getUrlPicture(id) {
      return `${SiteUrl.getPicture()}${id}`;
    },
    OpenPicture(id) {
      this.openPicture = this.getUrlPicture(id);
      this.dialog=true;
    },
  },
};
</script>