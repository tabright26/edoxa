const items = [
  {
    title: true,
    name: "Arena",
    show: true
  },
  {
    name: "Challenges",
    url: "/arena/challenges",
    icon: "fa fa-trophy",
    show: true
  },
  {
    name: "Tournaments",
    url: "/arena/tournaments",
    icon: "fa fa-sitemap",
    show: false
  },
  {
    name: "Games",
    url: "/arena/games",
    icon: "fa fa-gamepad",
    show: true
  },
  {
    title: true,
    name: "Organizations",
    show: true
  },
  {
    name: "Clans",
    url: "/structures/clans",
    icon: "fa fa-users",
    show: true
  },
  {
    name: "Teams",
    url: "/structures/teams",
    icon: "icon-cursor",
    show: false
  },
  {
    name: "Leagues",
    url: "/structures/leagues",
    icon: "icon-cursor",
    show: false
  },
  {
    divider: true,
    class: "mt-auto",
    show: true
  },
  {
    name: "F. A. Q.",
    url: "/faq",
    icon: "fa fa-question-circle",
    variant: "primary",
    show: true
  }
];

export default {
  items: items.filter(nav => nav.show)
};
