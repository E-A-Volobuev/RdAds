<template>
  <v-container>
    <v-row style="height: 150px"></v-row>
    <v-row justify="center">
      <v-col cols="4">
        <v-form ref="form" v-model="valid" lazy-validation>
          <v-text-field
            v-model="name"
            :counter="20"
            :rules="nameRules"
            label="Имя"
            required
          ></v-text-field>

          <v-text-field
            v-model="email"
            :rules="emailRules"
            label="E-mail"
            required
          ></v-text-field>

          <v-text-field
            v-model="phone"
            :rules="phoneRules"
            label="Телефон"
            required
          ></v-text-field>

          <v-text-field
            v-model="password"
            :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
            :rules="rulesPassword"
            :type="show1 ? 'text' : 'password'"
            label="Придумайте и введите пароль"
            required
            @click:append="show1 = !show1"
          ></v-text-field>

          <v-text-field
            v-model="passwordConfirm"
            :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
            :rules="rulesPassword"
            :type="show2 ? 'text' : 'password'"
            label="Введите пароль повторно"
            required
            @click:append="show2 = !show2"
          ></v-text-field>
          <p></p>
          <p></p>
          <p></p>
          <p></p>

          <v-file-input
            dense
            outlined
            label="Выберите аватар"
            v-model="avatar"
            accept=".jpg, .png"
            hide-details
            class="mx-2"
            @change="onFileChange(avatar)"
          ></v-file-input>
          <p></p>
          <p></p>
          <p></p>
          <p></p>
          <img v-if="url" :src="url" width="200" height="150" />
          <p></p>
          <p></p>
          <v-btn
            :disabled="!valid"
            color="primary"
            class="mr-4"
            @click="fetchSendObject()"
            :loading="loading"
          >
            Регистрация
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
    nameRules: [
      (v) => !!v || "Не указано имя пользователя",
      (v) => (v && v.length <= 20) || "Имя должно иметь не более 20 символов",
    ],
    email: "",
    emailRules: [
      (v) => !!v || "Введите e-mail",
      (v) => /.+@.+\..+/.test(v) || "Некорректный e-mail",
    ],
    phone: "",
    phoneRules: [
      (v) => !!v || "Не указан номер телефона",
      (v) =>
        /^\+?[78][-\(]?\d{3}\)?-?\d{3}-?\d{2}-?\d{2}$/.test(v) ||
        "Некорректный номер телефона",
    ],
    show1: false,
    show2: false,
    password: "",
    passwordConfirm: "",
    rulesPassword: [
      (v) => !!v || "Введите пароль",
      (v) => v.length >= 8 || "Пароль должен содержать не менее 8 символов",
    ],
    url: null,
    avatar: null,
    loading: false,
  }),
  mounted: function () {
    this.validate();
  },
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    async fetchSendObject() {
      if (this.password != this.passwordConfirm) {
        alert("пароли не совпадают");
      } else {
        this.loading = true;
        const r = await fetch(SiteUrl.userRegister(), {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },

          body: JSON.stringify({
            Name: this.name,
            Email: this.email,
            Phone: this.phone,
            Password: this.password,
            PasswordConfirm: this.passwordConfirm,
          })
        });

        if (r.ok === true) {
          let id = await r.json();
          this.fetchAddPhoto(id);
        } else {
          let error=await r.json();
          alert(error);
        }
      }
    },
    async fetchAddPhoto(id) {
      let formData = new FormData();
      formData.append("id", id);
      formData.append("photo", this.avatar);

      const response = await fetch(SiteUrl.addAvatarUser(), {
        method: "POST",
        body: formData,
      });

      if (response.ok === true) {
        this.loading = false;
        this.$router.push(`/jwt`);
      } else {
        alert("ошибка регистрации");
      }
    },
    onFileChange(file) {
      if (file === null) this.url = null;
      else this.url = URL.createObjectURL(file);
    },
  },
};
</script>