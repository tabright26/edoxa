import React, { FunctionComponent, useState } from "react";
import { CardImg, CardImgOverlay, Card } from "reactstrap";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import {
  LINK_GAME_CREDENTIAL_MODAL,
  UNLINK_GAME_CREDENTIAL_MODAL
} from "utils/modal/constants";
import { show } from "redux-modal";
import { compose } from "recompose";
import { withUserProfileGameIsAuthenticated } from "utils/oidc/containers";
import { RootState } from "store/types";
import { HocUserProfileGameIsAuthenticatedStateProps } from "utils/oidc/containers/types";
import { GameOptions, Game } from "types/games";

const style: React.CSSProperties = {
  filter: "brightness(50%)",
  borderRadius: "25px"
};

type OwnProps = {
  game: Game;
};

type StateProps = {
  gameOptions: GameOptions;
};

interface DispatchProps {
  showLinkGameAccountCredentialModal: () => void;
  showUnlinkGameAccountCredentialModal: () => void;
}

type InnerProps = StateProps &
  DispatchProps &
  HocUserProfileGameIsAuthenticatedStateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Item: FunctionComponent<Props> = ({
  gameOptions,
  showLinkGameAccountCredentialModal,
  showUnlinkGameAccountCredentialModal,
  isAuthenticated
}) => {
  const [hover, setHover] = useState(false);
  const filter = !isAuthenticated ? "grayscale(100%)" : null;
  return (
    <Card
      className="p-0 my-0 col-6"
      onMouseEnter={() => setHover(true)}
      onMouseLeave={() => setHover(false)}
      onClick={() =>
        isAuthenticated
          ? showUnlinkGameAccountCredentialModal()
          : showLinkGameAccountCredentialModal()
      }
      style={
        hover
          ? { cursor: "pointer", borderRadius: "25px" }
          : { borderRadius: "25px" }
      }
    >
      <CardImg
        src={require(`assets/img/arena/games/${gameOptions.name.toLowerCase()}/panel.jpg`)}
        style={hover ? style : { borderRadius: "25px", filter }}
      />
      <CardImgOverlay className="d-flex" style={{ filter }}>
        {hover ? (
          isAuthenticated ? (
            <h5 className="m-auto">UNLINK MY GAME ACCOUNT...</h5>
          ) : (
            <h5 className="m-auto">LINK MY GAME ACCOUNT...</h5>
          )
        ) : (
          <img
            src={require(`assets/img/arena/games/${gameOptions.name.toLowerCase()}/large.png`)}
            alt="leagueoflegends"
            className="m-auto"
          />
        )}
      </CardImgOverlay>
    </Card>
  );
};

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  return {
    gameOptions: state.static.games.games.find(
      x => x.name.toUpperCase() === ownProps.game.toUpperCase()
    )
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch,
  ownProps
) => {
  return {
    showLinkGameAccountCredentialModal: () =>
      dispatch(show(LINK_GAME_CREDENTIAL_MODAL, { game: ownProps.game })),
    showUnlinkGameAccountCredentialModal: () =>
      dispatch(
        show(UNLINK_GAME_CREDENTIAL_MODAL, {
          game: ownProps.game
        })
      )
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps, mapDispatchToProps),
  withUserProfileGameIsAuthenticated
);

export default enhance(Item);
