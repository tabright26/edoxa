import React, { useState } from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { LINK_GAME_CREDENTIAL_MODAL } from "modals";
import GameAuthenticationFrom from "components/Game/Authentication/Form";
import { compose } from "recompose";

// TODO: FRANCIS NEED PROCESS DETAILS.

const LinkGameAuthenticationModal = ({ show, handleHide, gameOption }) => {
  const [authenticationFactor, setAuthenticationFactor] = useState(null);
  return (
    <Modal className="modal-dialog-centered" isOpen={show} toggle={handleHide}>
      <ModalHeader toggle={handleHide}>
        <strong>{gameOption.displayName} Authentications</strong>
      </ModalHeader>
      <ModalBody>
        {!authenticationFactor ? (
          <GameAuthenticationFrom.Generate
            game={gameOption.name}
            setAuthenticationFactor={setAuthenticationFactor}
          />
        ) : (
          <>
            <div className="d-flex justify-content-around">
              <div className="text-center">
                <h5>Current</h5>
                <img
                  src={authenticationFactor.currentSummonerProfileIconBase64}
                  alt="current"
                  height={100}
                  width={100}
                />
              </div>
              <div className="text-center">
                <h5>Expected</h5>
                <img
                  src={authenticationFactor.expectedSummonerProfileIconBase64}
                  alt="expected"
                  height={100}
                  width={100}
                />
              </div>
            </div>
            <div className="d-flex justify-content-center mt-3">
              <GameAuthenticationFrom.Validate
                game={gameOption.name}
                handleCancel={handleHide}
                setAuthenticationFactor={setAuthenticationFactor}
              />
            </div>
          </>
        )}
      </ModalBody>
    </Modal>
  );
};

//Todo: This is an hard fixed modal. For some reason, when we use modal with the destroy on hide, the page crash when we cancel or hide the modal. Due
// to not having the props anymore or because the props have been destroyed ????
const enhance = compose<any, any>(
  connectModal({ name: LINK_GAME_CREDENTIAL_MODAL, destroyOnHide: false })
);

export default enhance(LinkGameAuthenticationModal);
