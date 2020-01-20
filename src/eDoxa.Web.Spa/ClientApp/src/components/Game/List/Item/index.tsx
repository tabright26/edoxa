import React, { FunctionComponent, useState } from "react";
import { CardImg, CardImgOverlay, Card } from "reactstrap";
import { GameOption } from "types";
import { connect, MapDispatchToProps } from "react-redux";
import {
  LINK_GAME_CREDENTIAL_MODAL,
  UNLINK_GAME_CREDENTIAL_MODAL
} from "utils/modal/constants";
import { show } from "redux-modal";

const style: React.CSSProperties = {
  filter: "brightness(50%)",
  borderRadius: "25px"
};

interface OwnProps {}

interface DispatchProps {
  showLinkGameAccountCredentialModal: (gameOption: GameOption) => any;
  showUnlinkGameAccountCredentialModal: (gameOption: GameOption) => any;
}

interface Props {
  gameOption: GameOption;
  showLinkGameAccountCredentialModal: (gameOption: GameOption) => any;
  showUnlinkGameAccountCredentialModal: (gameOption: GameOption) => any;
}

const Item: FunctionComponent<Props> = ({
  gameOption,
  showLinkGameAccountCredentialModal,
  showUnlinkGameAccountCredentialModal
}) => {
  const [hover, setHover] = useState(false);
  const filter = !gameOption.verified ? "grayscale(100%)" : null;
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
        style={hover ? style : { borderRadius: "25px", filter }}
      />
      <CardImgOverlay className="d-flex" style={{ filter }}>
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

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch,
  ownProps
) => {
  return {
    showLinkGameAccountCredentialModal: (gameOption: GameOption) =>
      dispatch(show(LINK_GAME_CREDENTIAL_MODAL, { gameOption })),
    showUnlinkGameAccountCredentialModal: (gameOption: GameOption) =>
      dispatch(show(UNLINK_GAME_CREDENTIAL_MODAL, { gameOption }))
  };
};

export default connect(null, mapDispatchToProps)(Item);
