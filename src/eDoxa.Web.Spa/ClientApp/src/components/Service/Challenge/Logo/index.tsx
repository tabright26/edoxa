import React, { FunctionComponent } from "react";
import { Card, CardImg } from "reactstrap";
import logo from "assets/img/arena/games/leagueoflegends/small.png";

type Props = {
  readonly className?: string;
  readonly height?: number;
  readonly width?: number;
};

const Logo: FunctionComponent<Props> = ({
  className = null,
  height = 200,
  width = 200
}) => (
  <Card className={`my-0 h-100 bg-gray-900 ${className}`}>
    <CardImg className="p-4 m-auto" src={logo} style={{ height, width }} />
  </Card>
);

export default Logo;
