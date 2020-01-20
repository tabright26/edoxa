import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { UNLINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";
import GameCredentialFrom from "components/Game/Credential/Form";
import { compose } from "recompose";
import { GameOptions } from "types";

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
  <Modal
    unmountOnClose={false}
    backdrop="static"
    centered
    isOpen={show}
    toggle={handleHide}
  >
    <ModalHeader toggle={handleHide}>
      <strong>Unlink your {gameOptions.displayName} credential?</strong>
    </ModalHeader>
    <ModalBody>
      <p>You can unlink your credential once a month.</p>
      <GameCredentialFrom.Unlink
        game={gameOptions.name}
        handleCancel={handleHide}
      />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: UNLINK_GAME_CREDENTIAL_MODAL, destroyOnHide: false })
);

export default enhance(Unlink);
