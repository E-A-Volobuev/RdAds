<template>
  <div class="">
    <div v-if="isEdit === 'true'">
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Вид услуги:"
            v-model="ad.typeItem"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Тип услуги:"
            v-model="ad.categoryItem"
          ></v-text-field>
        </v-col>
      </v-row>
    </div>
    <div v-else>
      <div class="text--primary">Вид услуги: {{ ad.typeItem }}</div>
      <div class="text--primary">Тип услуги: {{ ad.categoryItem }}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "service",
  props: ["ad", "isEdit"],
  data: () => ({}),
  methods: {},
};
</script>