import React from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { UNLINK_GAME_ACCOUNT_CREDENTIAL_MODAL } from "modals";
import GameAccountCredentialFrom from "components/Game/Account/Credential/Form";
import { compose } from "recompose";

const GenerateGameAuthFactorModal = ({ show, handleHide, gameOption }) => (
  <Modal className="modal-dialog-centered" isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>
      <strong>{gameOption.displayName} Authentications</strong>
    </ModalHeader>
    <ModalBody>
      <GameAccountCredentialFrom.Unlink
        game={gameOption.name}
        handleCancel={handleHide}
      />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(
  connectModal({ name: UNLINK_GAME_ACCOUNT_CREDENTIAL_MODAL })
);

export default enhance(GenerateGameAuthFactorModal);
