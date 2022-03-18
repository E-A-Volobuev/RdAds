<template>
  <div class="">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Производитель:"
            v-model="ad.brand"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-combobox
            v-model="ad.сondition"
            :items="conditions"
            label="Состояние:"
            outlined
            dense
          ></v-combobox>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-combobox
            v-model="ad.categoryItem"
            :items="category"
            label="Категория:"
            outlined
            dense
          ></v-combobox>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-combobox
            v-model="ad.availability"
            :items="availabilities"
            label="Наличие:"
            outlined
            dense
          ></v-combobox>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "electronic-ad",
  props: ["ad","conditions","availabilities"],
  data: () => ({
    category:["Аудио и видео","Игры,приставки и программы","Настольные компьютеры","Ноутбуки",
    "Оргтехника и расходники","Планшеты и электронные книги","Телефоны","Товары для компьютера","Фототехника","Смарт часы"],
  }),
  methods: {},
};
</script>