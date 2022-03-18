<template>
  <div class="">
    <v-container>
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
          <v-text-field
            dense
            label="Производитель:"
            v-model="ad.brand"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Тип:"
            v-model="ad.typeItem"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Тип запчасти:"
            v-model="ad.typeSparePartsAndAcsessories"
          ></v-text-field>
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
  name: "spareParts-ad",
  props: ["ad","conditions","availabilities"],
  data: () => ({
    category:["Запчасти","Аксессуары","Экипировка","Шины, диски, колёса","Тюнинг","Прицепы","Инструменты"]
  }),
  methods: {},
};
</script>