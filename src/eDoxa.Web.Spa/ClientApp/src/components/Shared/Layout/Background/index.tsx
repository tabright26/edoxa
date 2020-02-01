import React, { FunctionComponent } from "react";
import { Card, CardImg, CardImgOverlay } from "reactstrap";
import panel from "assets/img/panel.jpg";

export const Background: FunctionComponent = ({ children }) => (
  <Card className="m-0 border-0">
    <CardImg
      src={panel}
      className="app"
      style={{ filter: "brightness(75%)" }}
    />
    <CardImgOverlay className="app flex-row align-items-center">
      {children}
    </CardImgOverlay>
  </Card>
);
