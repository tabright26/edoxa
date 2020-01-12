import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { UNLINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";
import GameCredentialFrom from "components/Game/Credential/Form";
import { compose } from "recompose";
import { GameOption } from "types";

type InnerProps = InjectedProps & {
  gameOption: GameOption;
};

type OutterProps = {};

type Props = InnerProps & OutterProps;

const CustomModal: FunctionComponent<Props> = ({
  show,
  handleHide,
  gameOption
}) => (
  <Modal
    unmountOnClose={false}
    backdrop="static"
    centered
    isOpen={show}
    toggle={handleHide}
  >
    <ModalHeader toggle={handleHide}>
      <strong>
        Are you sure to unlink {gameOption.displayName} credential?
      </strong>
    </ModalHeader>
    <ModalBody>
      <GameCredentialFrom.Unlink
        game={gameOption.name}
        handleCancel={handleHide}
      />
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: UNLINK_GAME_CREDENTIAL_MODAL, destroyOnHide: false })
);

export default enhance(CustomModal);
