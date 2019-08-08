import React from "react";
import { Card } from "react-bootstrap";

import logo from "../../../assets/img/games/LeagueOfLegends.png";

const ArenaChallengeLogo = () => (
  <Card className="bg-dark mx-3 my-4">
    <Card.Img className="p-4" src={logo} />
  </Card>
);

export default ArenaChallengeLogo;
