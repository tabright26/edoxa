import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { LINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";
import { compose } from "recompose";
import { ModalSubtitle } from "components/Shared/Modal/Subtitle";
import { GameOptions, Game } from "types/games";
import Workflow from "components/Service/Game/Workflow";
import { MapStateToProps, connect } from "react-redux";
import { RootState } from "store/types";

type OwnProps = {
  game: Game;
};

type StateProps = { gameOptions: GameOptions };

type InnerProps = StateProps & InjectedProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Link: FunctionComponent<Props> = ({ show, handleHide, gameOptions }) => (
  <Modal backdrop="static" centered isOpen={show} toggle={handleHide}>
    <ModalHeader className="my-auto" toggle={handleHide}>
      <strong className="text-uppercase">
        {gameOptions.displayName} Authentications
      </strong>
      <ModalSubtitle>
        We need to link your primary account summoner name to automatically
        validate your in-game score and to verify the account ownership.
      </ModalSubtitle>
    </ModalHeader>
    <ModalBody>
      <Workflow show={show} handleHide={handleHide} game={gameOptions.name} />
    </ModalBody>
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
  connectModal({ name: LINK_GAME_CREDENTIAL_MODAL, destroyOnHide: false }),
  connect(mapStateToProps)
);

export default enhance(Link);
