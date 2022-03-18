<template>
  <div class="">
    <div v-if="isEdit === 'true'">
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
    </div>
    <div v-else>
      <div class="text--primary">Вид товара: {{ ad.typeItem }}</div>
      <div class="text--primary">Состояние: {{ ad.condition }}</div>
      <div class="text--primary">Категория: {{ ad.categoryItem }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "home-Show",
  props: ["ad","conditions","isEdit"],
  data: () => ({
    category:["Бытовая техника","Мебель и интерьер","Посуда и товары для кухни","Продукты питания","Ремонт и строительство","Растения"]
  }),
  methods: {},
};
</script>