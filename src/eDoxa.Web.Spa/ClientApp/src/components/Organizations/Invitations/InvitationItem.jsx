import React, { Fragment } from "react";
import { toastr } from "react-redux-toastr";
import { Col, Button } from "reactstrap";

const InvitationItem = ({ actions, invitation, type }) => {
  return (
    <Fragment>
      <Col>{type === "user" ? invitation.clanName : invitation.doxaTag}</Col>
      {type === "user" ? (
        <Col>
          <Button
            color="info"
            onClick={() =>
              actions
                .acceptInvitation(invitation.id)
                .then(toastr.success("SUCCESS", "Invitation was accepted."))
                .catch(toastr.error("WARNINGAVERTISSEMENTAVECLELOGODUFBIQUIDECOLEPUAVANTLEFILM", "Invitation not was accepted."))
            }
          >
            Accept invitation
          </Button>
          <Button
            color="danger"
            onClick={() =>
              actions
                .declineInvitation(invitation.id)
                .then(toastr.success("SUCCESS", "Invitation was accepted."))
                .catch(toastr.error("WARNINGAVERTISSEMENTAVECLELOGODUFBIQUIDECOLEPUAVANTLEFILM", "Invitation not was accepted."))
            }
          >
            Decline invitation
          </Button>
        </Col>
      ) : null}
      ;
    </Fragment>
  );
};

export default InvitationItem;
