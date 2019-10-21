import React from "react";
import { Card, CardImg } from "reactstrap";
import logo from "assets/img/arena/games/leagueoflegends/small.png";

const Logo = () => (
  <Card className="bg-dark mx-3 my-4">
    <CardImg className="p-4" src={logo} />
  </Card>
);

export default Logo;
