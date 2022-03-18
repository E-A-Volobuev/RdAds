<template>
  <div v-if="email === ''">войдите или зарегистрируйтесь</div>
  <div v-else>
    <v-container>
      <v-row justify="center">
        <v-col cols="auto">
          <v-form ref="form" v-model="valid" lazy-validation>
            <v-card class="mx-auto" min-width="800">
              <v-card-title>
                <v-row>
                  <v-col md="4" align-self="center"> </v-col>
                  <v-col>
                    <v-avatar color="primary" size="200"
                      ><v-img :src="avatar"> </v-img
                    ></v-avatar>
                  </v-col>
                  <v-col md="4"></v-col>
                </v-row>
              </v-card-title>
              <v-card-title>
                <v-row>
                  <v-col md="2" align-self="center"
                    ><v-icon x-large> mdi-camera-plus </v-icon>
                  </v-col>
                  <v-col md="8">
                    <v-file-input
                      dense
                      label="Выберите другое фото"
                      v-model="photo"
                      accept=".jpg, .png"
                      hide-details
                      class="mx-2"
                      @change="onFileChange(photo)"
                    ></v-file-input>
                  </v-col>
                  <v-col md="2"></v-col>
                </v-row>
              </v-card-title>
              <v-card-title>
                <v-row>
                  <v-col md="2" align-self="center">
                    <v-icon x-large> mdi-card-account-details-outline </v-icon>
                  </v-col>
                  <v-col md="8">
                    <v-text-field
                      v-model="user.name"
                      label="Имя пользователя"
                      :rules="nameRules"
                      required
                      outlined
                    ></v-text-field>
                  </v-col>
                  <v-col md="2"></v-col>
                </v-row>
              </v-card-title>
              <v-card-title>
                <v-row>
                  <v-col md="2" align-self="center">
                    <v-icon x-large> mdi-email-edit-outline </v-icon>
                  </v-col>
                  <v-col md="8">
                    <v-text-field
                      v-model="user.email"
                      :rules="emailRules"
                      label="E-mail"
                      required
                      outlined
                    ></v-text-field>
                  </v-col>
                  <v-col md="2"></v-col>
                </v-row>
              </v-card-title>
              <v-card-title>
                <v-row>
                  <v-col md="2" align-self="center">
                    <v-icon x-large> mdi-phone-check </v-icon>
                  </v-col>
                  <v-col md="8">
                    <v-text-field
                      v-model="user.phone"
                      :rules="phoneRules"
                      label="Телефон"
                      required
                      outlined
                    ></v-text-field>
                  </v-col>
                  <v-col md="2"></v-col>
                </v-row>
              </v-card-title>
              <v-card-title>
                <v-row>
                  <v-col md="2" align-self="center">
                    <v-icon x-large> mdi-lock-alert-outline </v-icon>
                  </v-col>
                  <v-col md="8">
                    <v-text-field
                      v-model="user.password"
                      :rules="rulesPassword"
                      :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                      :type="show1 ? 'text' : 'password'"
                      label="пароль"
                      required
                      @click:append="show1 = !show1"
                      outlined
                    ></v-text-field>
                  </v-col>
                  <v-col md="2"></v-col>
                </v-row>
              </v-card-title>
              <v-card-actions>
                <v-row justify="center">
                  <v-col cols="auto">
                    <v-btn
                      rounded
                      color="primary"
                      dark
                      @click="fetchSendObject"
                      :loading="loading"
                    >
                      сохранить
                    </v-btn>
                  </v-col>
                </v-row>
              </v-card-actions>
            </v-card>
          </v-form>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import SiteUrl from "../settings/site-url.settings";

export default {
  data: () => ({
    user: {},
    show1: false,
    nameRules: [
      (v) => !!v || "Не указано имя пользователя",
      (v) => (v && v.length <= 20) || "Имя должно иметь не более 20 символов",
    ],
    emailRules: [
      (v) => !!v || "Введите e-mail",
      (v) => /.+@.+\..+/.test(v) || "Некорректный e-mail",
    ],
    phoneRules: [
      (v) => !!v || "Не указан номер телефона",
      (v) =>
        /^\+?[78][-\(]?\d{3}\)?-?\d{3}-?\d{2}-?\d{2}$/.test(v) ||
        "Некорректный номер телефона",
    ],
    rulesPassword: [(v) => !!v || "Введите пароль"],
    valid: true,
    avatar: "",
    oldAvatar: "",
    photo: null,
    loading: false,
  }),
  computed: {
    email: function () {
      return this.$store.getters.getEmail;
    },
  },
  mounted: function () {
    this.validate();
  },
  created() {
    this.fetchAvatarUser();
    this.fetchCurrentUser();
  },
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    async fetchAvatarUser() {
      const token = sessionStorage.getItem("accessToken");
      const response = await fetch(SiteUrl.getAvatarUser(), {
        method: "GET",
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + token,
        },
      });
      if (response.ok === true) {
        let r = await response.blob();
        this.avatar = URL.createObjectURL(r);
        this.oldAvatar = this.avatar;
      } else console.log("Status: ", response.status);
    },
    onFileChange(file) {
      if (file === null) this.avatar = this.oldAvatar;
      else this.avatar = URL.createObjectURL(file);
    },
    async fetchCurrentUser() {
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
        this.user = data;
      } else console.log("Status: ", response.status);
    },
    async fetchSendObject() {

      this.loading = true;
      const token = sessionStorage.getItem("accessToken");
      const r = await fetch(SiteUrl.editCurrentUser(), {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },

        body: JSON.stringify({
          Id: this.user.id,
          Name: this.user.name,
          Email: this.user.email,
          Phone: this.user.phone,
          Password: this.user.password,
        }),
      });

      if (r.ok === true) {
        let id = await r.json();
        console.log(id);
        this.loading = false;
        //this.fetchAddPhoto(id);
      } else {
        let error = await r.json();
        alert(error);
      }
    },
  },
};
</script>