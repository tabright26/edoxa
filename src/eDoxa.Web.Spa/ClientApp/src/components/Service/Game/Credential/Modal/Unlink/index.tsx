import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalHeader } from "reactstrap";
import { UNLINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";
import GameCredentialFrom from "components/Service/Game/Credential/Form";
import { compose } from "recompose";
import { GameOptions, Game } from "types/games";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";

type OwnProps = {
  game: Game;
};

type StateProps = { gameOptions: GameOptions };

type InnerProps = InjectedProps & {
  gameOptions: GameOptions;
};

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Unlink: FunctionComponent<Props> = ({
  show,
  handleHide,
  gameOptions
}) => (
  <Modal backdrop="static" centered isOpen={show} toggle={handleHide}>
    <ModalHeader className="text-uppercase my-auto " toggle={handleHide}>
      Unlink your {gameOptions.displayName} credential?
    </ModalHeader>
    {show && (
      <GameCredentialFrom.Unlink
        game={gameOptions.name}
        handleCancel={handleHide}
      />
    )}
  </Modal>
);

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

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: UNLINK_GAME_CREDENTIAL_MODAL, destroyOnHide: false }),
  connect(mapStateToProps)
);

export default enhance(Unlink);
