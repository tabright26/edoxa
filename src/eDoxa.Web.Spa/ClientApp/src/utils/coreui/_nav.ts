import {
  getChallengesPath,
  getClansPath,
  getFaqPath
} from "utils/coreui/constants";

const items = [
  {
    title: true,
    name: "Arena",
    show: true
  },
  {
    name: "Challenges",
    url: getChallengesPath(),
    icon: "fa fa-trophy",
    show: true
  },
  {
    title: true,
    name: "Organizations",
    show: false
  },
  {
    name: "Clans",
    url: getClansPath(),
    icon: "fa fa-users",
    show: false
  },
  {
    divider: true,
    class: "mt-auto",
    show: true
  },
  {
    name: "FAQs",
    url: getFaqPath(),
    icon: "fa fa-question-circle",
    show: true
  }
];

export default {
  items: items.filter(nav => nav.show)
};
