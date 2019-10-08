import React from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_CLAN_MODAL } from "modals";
import ClanForm from "forms/Organizations/Clans";

const CreateClanModal = ({ show, handleHide, className, actions }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide} className={"modal-primary " + className}>
    <ModalHeader toggle={handleHide}>Create a new clan</ModalHeader>
    <ModalBody>
      <dl className="row mb-0">
        <dd className="col-sm-2 mb-0 text-muted">New Clan</dd>
        <dd className="col-sm-8 mb-0">
          <ClanForm.Create onSubmit={fields => actions.addClan(fields).then(() => handleHide())} handleCancel={handleHide} />
        </dd>
      </dl>
    </ModalBody>
  </Modal>
);

export default connectModal({ name: CREATE_CLAN_MODAL })(CreateClanModal);