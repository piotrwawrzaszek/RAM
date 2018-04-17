namespace RAM.Domain.Helpers
{
    public enum Instruction
    {
        Empty,
        LOAD,
        STORE,
        READ,
        WRITE,
        ADD,
        SUB,
        MULT,
        DIV,
        MOD,
        JUMP,
        JZERO,
        JGTZ,
        JLTZ,
        HALT
    }
}