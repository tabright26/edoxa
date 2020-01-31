import React, { FunctionComponent, useState } from "react";
import { Popover, PopoverHeader, PopoverBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faQuestionCircle } from "@fortawesome/free-solid-svg-icons";
import Popper from "popper.js";

interface Props {
  id: string;
  header: string;
  placement: Popper.Placement;
}

export const Info: FunctionComponent<Props> = ({
  id,
  header,
  placement,
  children
}) => {
  const [open, setOpen] = useState(false);
  return (
    <>
      <span id={id} className="ml-1 my-auto">
        <FontAwesomeIcon className="text-muted" icon={faQuestionCircle} />
      </span>
      <Popover
        style={{
          minWidth: "250px"
        }}
        placement={placement}
        isOpen={open}
        target={id}
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader className="text-uppercase">{header}</PopoverHeader>
        <PopoverBody>{children}</PopoverBody>
      </Popover>
    </>
  );
};
