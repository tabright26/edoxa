import React, { Fragment } from "react";
import { toastr } from "react-redux-toastr";
import { Button } from "reactstrap";

const InvitationListItem = ({ actions, invitation, type }) => {
  return (
    <Fragment>
      {type === "user" ? invitation.clanName : invitation.doxatag}
      {type === "user" && (
        <>
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
        </>
      )}
    </Fragment>
  );
};

export default InvitationListItem;
