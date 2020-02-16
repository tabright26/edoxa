import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalHeader } from "reactstrap";
import { UNLINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";
import GameCredentialFrom from "components/Service/Game/Credential/Form";
import { compose } from "recompose";
import { GameOptions } from "types/games";

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
    <ModalHeader
      className="text-uppercase my-auto bg-gray-900"
      toggle={handleHide}
    >
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

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: UNLINK_GAME_CREDENTIAL_MODAL, destroyOnHide: false })
);

export default enhance(Unlink);
