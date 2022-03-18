<template>
  <div class="">
    <div v-if="isEdit === 'true'">
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
    </div>
    <div v-else>
      <div class="text--primary">Вид оборудования: {{ ad.typeItem }}</div>
      <div class="text--primary">Состояние: {{ ad.сondition }}</div>
      <div class="text--primary">Категория: {{ ad.categoryItem }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "business-Show",
  props: ["ad","conditions","isEdit"],
  data: () => ({
    category:["Готовый бизнес","Оборудование для бизнеса"],
    typeObjects:["Для магазина","Для офиса","Для ресторана","Для салона красоты","Промышленное","Другое"]
  }),
  methods: {},
};
</script>