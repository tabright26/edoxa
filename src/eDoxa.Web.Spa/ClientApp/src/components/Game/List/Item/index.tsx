import React, { FunctionComponent, useState } from "react";
import { CardImg, CardImgOverlay, Card } from "reactstrap";
import { GameOptions, Game } from "types";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import {
  LINK_GAME_CREDENTIAL_MODAL,
  UNLINK_GAME_CREDENTIAL_MODAL
} from "utils/modal/constants";
import { show } from "redux-modal";
import { compose } from "recompose";
import {
  withUserProfileGameIsAuthenticated,
  HocUserProfileGameIsAuthenticatedStateProps
} from "utils/oidc/containers";
import { RootState } from "store/types";

const style: React.CSSProperties = {
  filter: "brightness(50%)",
  borderRadius: "25px"
};

interface OwnProps {
  gameOptions: GameOptions;
}

type StateProps = {
  game: Game;
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
      className="p-0 col-6"
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
  _state,
  ownProps
) => {
  return {
    game: ownProps.gameOptions.name
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch,
  ownProps
) => {
  return {
    showLinkGameAccountCredentialModal: () =>
      dispatch(
        show(LINK_GAME_CREDENTIAL_MODAL, { gameOptions: ownProps.gameOptions })
      ),
    showUnlinkGameAccountCredentialModal: () =>
      dispatch(
        show(UNLINK_GAME_CREDENTIAL_MODAL, {
          gameOptions: ownProps.gameOptions
        })
      )
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps, mapDispatchToProps),
  withUserProfileGameIsAuthenticated
);

export default enhance(Item);
