import React, { useState } from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { GENERATE_GAME_AUTH_FACTOR_MODAL } from "modals";
import AuthFactorFrom from "components/User/Game/AuthFactor/Form";
import CredentialFrom from "components/User/Game/Credential/Form";
import { compose } from "recompose";

const GenerateGameAuthFactorModal = ({ show, handleHide, game }) => {
  const [authFactor, setAuthFactor] = useState(null);
  return (
    <Modal className="modal-dialog-centered" isOpen={show} toggle={handleHide}>
      <ModalHeader toggle={handleHide}>
        <strong>{game} Authentications</strong>
      </ModalHeader>
      <ModalBody>
        {!authFactor ? (
          <AuthFactorFrom.Generate game={game} setAuthFactor={setAuthFactor} />
        ) : (
          <>
            <p className="text-muted text-justify">
              weqw ewqe qwe qwen iqwneinwq ienijwq nenwq einw ijneiqjw neijwqn
              ienqwi jeqwje niqjw neiwqjne iqjwneinqwie jnqwiej
              nqijwenqijwneijqwn eijqnweij nqijwne
            </p>
            <div className="d-flex justify-content-around">
              <div className="text-center">
                <h5>Current</h5>
                <img
                  src={authFactor.currentSummonerProfileIconBase64}
                  height={100}
                  width={100}
                />
              </div>
              <div className="text-muted text-center">
                <h5>Expected</h5>
                <img
                  src={authFactor.expectedSummonerProfileIconBase64}
                  height={100}
                  width={100}
                />
              </div>
            </div>
            <p className="text-justify mt-3">
              weqw ewqe qwe qwen iqwneinwq ienijwq nenwq einw ijneiqjw neijwqn
              ienqwi jeqwje niqjw neiwqjne iqjwneinqwie jnqwiej
              nqijwenqijwneijqwn eijqnweij nqijwne
            </p>
            <div className="d-flex justify-content-center mt-3">
              <CredentialFrom.Link game={game} setAuthFactor={setAuthFactor} />
            </div>
          </>
        )}
      </ModalBody>
    </Modal>
  );
};

const enhance = compose<any, any>(
  connectModal({ name: GENERATE_GAME_AUTH_FACTOR_MODAL })
);

export default enhance(GenerateGameAuthFactorModal);
