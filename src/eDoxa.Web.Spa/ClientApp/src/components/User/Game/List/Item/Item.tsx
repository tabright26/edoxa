import React, { FunctionComponent, useState } from "react";
import { CardImg, CardImgOverlay, Card, Button } from "reactstrap";
import { compose } from "recompose";
import { GameOption } from "types";
import { withModals } from "utils/modal/container";

const style = { filter: "brightness(50%)" };

interface Props {
  option: GameOption;
  modals: any;
}

const Item: FunctionComponent<Props> = ({ option, modals }) => {
  const [hover, setHover] = useState(false);
  return (
    <Card className={`p-0 col-6`} onMouseEnter={() => setHover(true)} onMouseLeave={() => setHover(false)}>
      <CardImg src={require(`assets/img/arena/games/${option.name.toLowerCase()}/panel.jpg`)} style={!hover ? style : null} />
      <CardImgOverlay className="d-flex">
        {hover ? (
          <Button color="primary" className="m-auto" size="lg" onClick={() => modals.showGenerateGameAuthFactorModal(option.name)}>
            LINK GAME CREDENTIAL
          </Button>
        ) : (
          <img src={require(`assets/img/arena/games/${option.name.toLowerCase()}/large.png`)} className="m-auto" />
        )}
      </CardImgOverlay>
    </Card>
  );
};

const enhance = compose<any, any>(withModals);

export default enhance(Item);
