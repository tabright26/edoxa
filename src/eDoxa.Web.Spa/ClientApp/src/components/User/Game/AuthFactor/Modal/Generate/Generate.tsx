import React from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { GENERATE_GAME_AUTH_FACTOR_MODAL } from "modals";
import GenerateGameAuthFactorForm from "components/User/Game/AuthFactor/Form/Generate";
import { compose } from "recompose";

const GenerateGameAuthFactorModal = ({ show, handleHide, game }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>Create auth factor</ModalHeader>
    <ModalBody>
      <dl className="row mb-0">
        <dd className="col-sm-2 mb-0 text-muted">New auth factor</dd>
        <dd className="col-sm-8 mb-0">
          <GenerateGameAuthFactorForm game={game} handleCancel={handleHide} />
        </dd>
      </dl>
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(connectModal({ name: GENERATE_GAME_AUTH_FACTOR_MODAL }));

export default enhance(GenerateGameAuthFactorModal);
