import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_CLAN_MODAL } from "utils/modal/constants";
import ClanForm from "components/Service/Clan/Form";
import { compose } from "recompose";
import { connect, DispatchProp } from "react-redux";

type InnerProps = DispatchProp &
  InjectedProps & {
    actions: any;
  };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({ show, handleHide, actions }) => (
  <Modal backdrop="static" size="lg" isOpen={show} toggle={handleHide} centered>
    <ModalHeader className="text-uppercase my-auto" toggle={handleHide}>
      Create a new clan
    </ModalHeader>
    <ModalBody>
      <dl className="row mb-0">
        <dd className="col-sm-2 mb-0 text-muted">New Clan</dd>
        <dd className="col-sm-8 mb-0">
          {show && (
            <ClanForm.Create
              onSubmit={fields =>
                actions.addClan(fields).then(() => handleHide())
              }
              handleCancel={handleHide}
            />
          )}
        </dd>
      </dl>
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connect(),
  connectModal({ name: CREATE_CLAN_MODAL, destroyOnHide: false })
);

export default enhance(Create);
