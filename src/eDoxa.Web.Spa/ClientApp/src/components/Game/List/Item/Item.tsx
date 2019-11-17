import React, { FunctionComponent, useState } from "react";
import { CardImg, CardImgOverlay, Card } from "reactstrap";
import { GameOption } from "types";

const style: React.CSSProperties = {
  filter: "brightness(50%)",
  borderRadius: "25px"
};

interface Props {
  gameOption: GameOption;
  showLinkGameAccountCredentialModal: (gameOption: GameOption) => any;
  showUnlinkGameAccountCredentialModal: (gameOption: GameOption) => any;
}

const GameListItem: FunctionComponent<Props> = ({
  gameOption,
  showLinkGameAccountCredentialModal,
  showUnlinkGameAccountCredentialModal
}) => {
  const [hover, setHover] = useState(false);
  return (
    <Card
      className="p-0 col-6"
      onMouseEnter={() => setHover(true)}
      onMouseLeave={() => setHover(false)}
      onClick={() =>
        gameOption.verified
          ? showUnlinkGameAccountCredentialModal(gameOption)
          : showLinkGameAccountCredentialModal(gameOption)
      }
      style={
        hover
          ? { cursor: "pointer", borderRadius: "25px" }
          : { borderRadius: "25px" }
      }
    >
      <CardImg
        src={require(`assets/img/arena/games/${gameOption.name.toLowerCase()}/panel.jpg`)}
        style={hover ? style : { borderRadius: "25px" }}
      />
      <CardImgOverlay className="d-flex">
        {hover ? (
          gameOption.verified ? (
            <h5 className="m-auto">UNLINK MY GAME ACCOUNT...</h5>
          ) : (
            <h5 className="m-auto">LINK MY GAME ACCOUNT...</h5>
          )
        ) : (
          <img
            src={require(`assets/img/arena/games/${gameOption.name.toLowerCase()}/large.png`)}
            alt="leagueoflegends"
            className="m-auto"
          />
        )}
      </CardImgOverlay>
    </Card>
  );
};

export default GameListItem;
