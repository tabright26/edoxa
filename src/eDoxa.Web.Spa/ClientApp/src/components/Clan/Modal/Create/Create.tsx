import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_CLAN_MODAL } from "utils/modal/constants";
import ClanForm from "components/Clan/Form";
import { compose } from "recompose";
import { connect, DispatchProp } from "react-redux";
import { destroy } from "redux-form";
import { CREATE_CLAN_FORM } from "utils/form/constants";

type InnerProps = DispatchProp &
  InjectedProps & {
    actions: any;
  };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const CustomModal: FunctionComponent<Props> = ({
  show,
  handleHide,
  actions,
  dispatch
}) => (
  <Modal
    unmountOnClose={false}
    backdrop="static"
    size="lg"
    isOpen={show}
    toggle={handleHide}
    centered
    onClosed={() => dispatch(destroy(CREATE_CLAN_FORM))}
  >
    <ModalHeader toggle={handleHide}>Create a new clan</ModalHeader>
    <ModalBody>
      <dl className="row mb-0">
        <dd className="col-sm-2 mb-0 text-muted">New Clan</dd>
        <dd className="col-sm-8 mb-0">
          <ClanForm.Create
            onSubmit={fields =>
              actions.addClan(fields).then(() => handleHide())
            }
            handleCancel={handleHide}
          />
        </dd>
      </dl>
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connect(),
  connectModal({ name: CREATE_CLAN_MODAL, destroyOnHide: false })
);

export default enhance(CustomModal);
