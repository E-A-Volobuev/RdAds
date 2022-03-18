<template>
  <div class="">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Гражданство:"
            v-model="ad.citizenship"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
           <v-text-field
            dense
            label="Сфера деятельности:"
            v-model="ad.sphereOfActivity"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="График работы: "
            v-model="ad.workingSchedule"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-text-field
            dense
            label="Опыт работы:"
            v-model="ad.workExperience"
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
           <v-combobox
            v-model="ad.sex"
            :items="sex"
            label="Пол:"
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
  name: "resume-ad",
  props: ["ad","sex"],
  data: () => ({}),
  methods: {},
};
</script>