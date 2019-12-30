import {
  getChallengesPath,
  getClansPath,
  getFaqPath
} from "utils/router/constants";

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
    show: false
  },
  {
    name: "F. A. Q.",
    url: getFaqPath(),
    icon: "fa fa-question-circle",
    variant: "primary",
    show: false
  }
];

export default {
  items: items.filter(nav => nav.show)
};
