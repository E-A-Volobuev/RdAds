<template>
  <v-container>
    <v-row style="height: 150px"></v-row>
    <v-row justify="center">
      <v-col cols="4">
        <v-form ref="form" v-model="valid" lazy-validation>
          <v-text-field v-model="name" label="Имя"></v-text-field>

          <v-text-field
            v-model="password"
            :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
            :type="show1 ? 'text' : 'password'"
            label="Введите пароль"
            required
            @click:append="show1 = !show1"
          ></v-text-field>
          <p></p>
          <p></p>
          <p></p>
          <p></p>
          <v-btn
            :disabled="!valid"
            color="primary"
            class="mr-4"
            @click="fetchSendObject"
          >
            Войти
          </v-btn>
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import SiteUrl from "../settings/site-url.settings";

export default {
  data: () => ({
    valid: true,
    name: "",
    show1: false,
    password: "",
    loading: false,
    avatar: null,
    email: "",
  }),
  watch: {
    email: function (setEmail) {
      this.$store.commit("setEmail", setEmail);
    },
    avatar: function (setAvatar) {
      this.$store.commit("setAvatar", setAvatar);
    },
  },
  methods: {
    async fetchSendObject() {
      this.loading = true;
      const formData = new FormData();
      formData.append("grant_type", "password");
      formData.append("username", this.name);
      formData.append("password", this.password);

      const r = await fetch(SiteUrl.jwtTest(), {
        method: "POST",
        body: formData,
      });

      if (r.ok === true) {
        this.loading = false;
        let item = await r.json();
        this.email = this.name;
        sessionStorage.setItem("accessToken", item.access_token);
        this.fetchAvatarUser();
      } else {
        alert("ошибка входа");
      }
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
        setTimeout(() => {
          this.$router.push({ path:"/" })
        }, 500);
      } else console.log("Status: ", response.status);
    }
  },
};
</script>