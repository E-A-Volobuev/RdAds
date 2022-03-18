<template>
  <div class="">
    <v-container>
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
    </v-container>
  </div>
</template>

<script>
export default {
  name: "animals-ad",
  props: ["ad"],
  data: () => ({
    category:["Собаки","Кошки","Птицы","Аквариум","Другие животные"]
  }),
  methods: {},
};
</script>