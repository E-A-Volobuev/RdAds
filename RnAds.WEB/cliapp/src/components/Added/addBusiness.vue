<template>
  <div class="">
    <v-container>
      <v-row>
        <v-col>
          <v-combobox
            v-model="ad.typeItem"
            :items="typeObjects"
            label="Вид оборудования:"
            outlined
            dense
          ></v-combobox>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-combobox
            v-model="ad.сondition"
            label="Состояние:"
            :items="conditions"
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
  name: "business-ad",
  props: ["ad","conditions"],
  data: () => ({
    category:["Готовый бизнес","Оборудование для бизнеса"],
    typeObjects:["Для магазина","Для офиса","Для ресторана","Для салона красоты","Промышленное","Другое"]
  }),
  methods: {},
};
</script>