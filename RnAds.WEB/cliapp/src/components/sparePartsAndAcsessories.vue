<template>
  <div class="">
    <div v-if="isEdit === 'true'">
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
          <v-text-field dense label="Тип:" v-model="ad.typeItem"></v-text-field>
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
    </div>
    <div v-else>
      <div class="text--primary">Состояние: {{ ad.condition }}</div>
      <div class="text--primary">Производитель: {{ ad.brand }}</div>
      <div class="text--primary">Категория: {{ ad.categoryItem }}</div>
      <div class="text--primary">Тип: {{ ad.typeItem }}</div>
      <div class="text--primary">
        Тип запчасти: {{ ad.typeSparePartsAndAcsessories }}
      </div>
      <div class="text--primary">Наличие: {{ ad.availability }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "spareParts",
  props: ["ad", "isEdit", "conditions", "availabilities"],
  data: () => ({
    category: [
      "Запчасти",
      "Аксессуары",
      "Экипировка",
      "Шины, диски, колёса",
      "Тюнинг",
      "Прицепы",
      "Инструменты",
    ],
  }),
  methods: {},
};
</script>