<template>
  <div>
    <div v-if="isEdit === 'true'">
      <v-row>
        <v-col md="2" align-self="center">
          <v-icon x-large> mdi-playlist-edit </v-icon>
        </v-col>
        <v-col md="8">
          <v-text-field
            label="Производитель:"
            outlined
            v-model="ad.brand"
          ></v-text-field>
        </v-col>
        <v-col md="2"></v-col>
      </v-row>
      <v-row>
        <v-col md="2" align-self="center">
          <v-icon x-large> mdi-new-box </v-icon>
        </v-col>
        <v-col md="8">
           <v-combobox
            v-model="ad.сondition"
            :items="conditions"
            label="Состояние:"
            outlined
          ></v-combobox>
        </v-col>
        <v-col md="2"></v-col>
      </v-row>
       <v-row>
        <v-col md="2" align-self="center">
          <v-icon x-large> mdi-shape </v-icon>
        </v-col>
        <v-col md="8">
            <v-combobox
            v-model="ad.categoryItem"
            :items="category"
            label="Категория:"
            outlined
            dense
          ></v-combobox>
        </v-col>
        <v-col md="2"></v-col>
      </v-row>
      <v-row>
        <v-col md="2" align-self="center">
          <v-icon x-large> mdi-store-alert </v-icon>
        </v-col>
        <v-col md="8">
            <v-combobox
            v-model="ad.availability"
            :items="availabilities"
            label="Наличие:"
            outlined
            dense
          ></v-combobox>
        </v-col>
        <v-col md="2"></v-col>
      </v-row>
    </div>
    <div v-else>
      <div class="text--primary">Состояние: {{ ad.condition }}</div>
      <div class="text--primary">Производитель: {{ ad.brand }}</div>
      <div class="text--primary">Категория: {{ ad.categoryItem }}</div>
      <div class="text--primary">Наличие: {{ ad.availability }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "electronics-Show",
  props: ["ad", "isEdit", "conditions", "availabilities"],
  data: () => ({
    category: [
      "Аудио и видео",
      "Игры,приставки и программы",
      "Настольные компьютеры",
      "Ноутбуки",
      "Оргтехника и расходники",
      "Планшеты и электронные книги",
      "Телефоны",
      "Товары для компьютера",
      "Фототехника",
    ],
  }),
  methods: {},
};
</script>
