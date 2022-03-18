<template>
  <div class="">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Вид:"
            v-model="ad.typeItem"
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
    </v-container>
  </div>
</template>

<script>
export default {
  name: "homeAndGarden-ad",
  props: ["ad","conditions"],
  data: () => ({
    category:["Бытовая техника","Мебель и интерьер","Посуда и товары для кухни","Продукты питания","Ремонт и строительство","Растения"]
  }),
  methods: {},
};
</script>