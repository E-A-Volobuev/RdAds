<template>
  <div class="">
    <div v-if="isEdit === 'true'">
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Порода:"
            v-model="ad.typeItem"
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
    </div>
    <div v-else>
      <div class="text--primary">Порода: {{ ad.typeItem }}</div>
      <div class="text--primary">Категория: {{ ad.categoryItem }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "animals-Show",
  props: ["ad","isEdit"],
  data: () => ({
    category:["Собаки","Кошки","Птицы","Аквариум","Другие животные"]
  }),
  methods: {},
};
</script>